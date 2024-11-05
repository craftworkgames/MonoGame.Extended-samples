// Copyright (c) Craftwork Games. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collections;

namespace Collections
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        
        private Bag<int> bag;       // Declare bag variable with type
        private Deque<int> deque;   // Declare deque variable with type
        // NOTE this is the (MonoGame.Extended.Collections.KeyedCollections), and not the one from Microsoft (System.Collections.ObjectModel.KeyedCollection)
        private KeyedCollection<int, MyEntity> keyedCollection;

        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Initialize a Bag collection of type Int with capacity (size) 3 (Default is 16)
            bag = new Bag<int>(3);
            deque = new Deque<int>();
            keyedCollection = new KeyedCollection<int, MyEntity>(e => e.Id);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // ##################################################################################
            // ####################               BAG EXAMPLE                ####################
            // ##################################################################################
            Debug.Print("Bag Example");

            // Add 3 integers to the bag
            bag.Add(4);
            bag.Add(8);
            bag.Add(15);

            // This is just so we can see the contents in the debug output.  This method doesn't exist, it is only in this example as an "Extension method".
            bag.DebugPrint();  // bag is now[4, 8, 15]

            bag.Add(16);
            bag.DebugPrint(); // bag is extended here, capacity is now 4 instead of 3 with items [4, 8, 15, 16]

            bag.RemoveAt(1);
            bag.DebugPrint(); // bag is now [4, 16, 15] with a capacity of 4

            bag.Remove(4);
            bag.DebugPrint(); // bag is now [15, 16] with a capacity of 4

            // ##################################################################################
            // ####################                DEQUE EXAMPLE             ####################
            // ##################################################################################
            Debug.Print("Deque Example");

            deque.AddToBack(1);
            deque.AddToBack(2);
            deque.AddToFront(4);
            deque.AddToFront(5);
            deque.AddToBack(3);

            // If you were keeping track that was 32145

            while (deque.Count > 0)
            {
                int item = deque.Pop();
                Debug.Print(item.ToString());
            }

            // ##################################################################################
            // ####################           KeyedCollection EXAMPLE        ####################
            // ##################################################################################
            Debug.Print("Keyed Collection Example");

            keyedCollection.Add(new MyEntity ( 1, "Player1" ));
            keyedCollection.Add(new MyEntity { Id = 2, Name = "Player2" });

            keyedCollection.TryGetValue(1, out MyEntity entity); // gets Player1
            Debug.Print(entity.Name);

            // ##################################################################################
            // ####################       ObservableCollection EXAMPLE       ####################
            // ##################################################################################
            ObservableCollection<int> observableCollection = new ObservableCollection<int>();

            // Wire up (Associate) all the methods to the event handlers
            observableCollection.ItemAdded += ItemAddedWatcher;
            observableCollection.ItemRemoved += ItemRemovedWatcher;
            observableCollection.Clearing += WatchedListClearing;
            observableCollection.Cleared += WatchedListCleared;

            observableCollection.Add(1);
            observableCollection.Add(2);
            observableCollection.Add(77);
            observableCollection.Add(3);
            observableCollection.Remove(2);
            observableCollection.Add(42);
            observableCollection.Clear();

            // Prints the following:
            /*
                Item Added: 1
                Item Added: 2
                Item Added: 77
                Item Added: 3
                Item Removed: 2
                Item Added: 42
                List is clearing....
                List is now cleared!
            */

            // ##################################################################################
            // ##################  ObservableCollection (Alternative) EXAMPLE  ##################
            // ##################################################################################
            // Here an existing list is passed in
            // Additionally, we use anonymous functions instead of methods
            List<int> sourceList = new List<int> {1, 2, 3, 55, 88, 13, 23, 42 };
            ObservableCollection<int> observableCollectionAlt = new ObservableCollection<int>(sourceList);

            // Wire up (Associate) all the anonymous functions to the event handlers
            observableCollectionAlt.ItemAdded += (sender, e) =>
            {
                Debug.Print($"Item Added: {e.Item}");
            };
            observableCollectionAlt.ItemRemoved += (sender, e) =>
            {
                Debug.Print($"Item Removed: {e.Item}");
            };
            observableCollectionAlt.Clearing += (sender, args) =>
            {
                Debug.Print("List is clearing....");
            };
            observableCollectionAlt.Cleared += (sender, args) =>
            {
                Debug.Print("List is now cleared!");
            };

            observableCollectionAlt.Add(7);
            observableCollectionAlt.Add(412);
            observableCollectionAlt.Remove(88);
            observableCollectionAlt.RemoveAt(3);
            observableCollectionAlt.Clear();

            // Prints the following:
            /*
             Item Added: 7
             Item Added: 412
             Item Removed: 88
             Item Removed: 55
             List is clearing....
             List is now cleared!
             */

            // ##################################################################################
            // ######################                Pool                  ######################
            // ##################################################################################

            // Create the pool with capacity 10
            Pool<Enemy> enemyPool = new Pool<Enemy>(
                createItem: () => new Enemy(),              // Function that will be executed when we need to create a new Enemy
                resetItem: enemy => enemy.Reset(),          // Method that will be executed when the Enemy is returned to the pool for re-use
                capacity: 10                                // Maximum pool capacity, can not grow
            );

            // Setup for your level
            List<Enemy> enemies = new List<Enemy>();        // Create a list to hold our alive "pooled" enemies

            // Enemy spawns
            Enemy enemy = enemyPool.Obtain();               // Get an object off the pool, or create a new one if none exist in the pool
            enemies.Add(enemy);                             // Add the enemy created to our alive list

            // Enemy is dead now, send back to pool
            enemyPool.Free(enemy);                          // Send the enemy back to the pool to reuse the memory later
            enemies.Remove(enemy);                          // remove the reference we had to the enemy in the "alive" list.

            // ##################################################################################
            // ######################            ObjectPool                ######################
            // ##################################################################################

            int startingCapacity = 10; // the default is 16
            var enemtyObjectPool = new ObjectPool<EnemyPoolable>(
                instantiationFunc: () => new EnemyPoolable(),
                capacity: startingCapacity
            );

            // If an enemy exists in the pool, we will reuse it, otherwise, we will create a new one
            var myNewEnemy = enemtyObjectPool.New();

            // Do whatever you need to the enemy (Update, draw, etc)

            // If the enemy is dead, now we would return it to the pool so it can be reused later
            myNewEnemy.Return();


            // ##################################################################################
            // ######################      Extension: list.Shuffle         ######################
            // ##################################################################################

            Random random = new Random();
            List<int> nums = new List<int> { 1, 2, 3, 4, 5 };

            Debug.Print("Before List Shuffle: " + '[' + string.Join(",", nums.Select(item => item.ToString())) + ']');

            // The Extension method for List `Shuffle` called
            nums.Shuffle(random);

            Debug.Print("After List Shuffle: " + '[' + string.Join(",", nums.Select(item => item.ToString())) + ']');

            // ##################################################################################
            // ######################  Extension: dict.GetValueOrDefault   ######################
            // ##################################################################################

            Dictionary<string, int> zipCodeLookupByCity = new Dictionary<string, int>();

            // The Extension method for dictionary to return a default if no key found
            int zipCode = zipCodeLookupByCity.GetValueOrDefault("typo city name", 90210);
            Debug.Print("Zip code is defaulted: " + zipCode);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        // ObservableCollection methods that are called when the specific actions happen to the collection being watched.
        public void ItemAddedWatcher(object sender, ItemEventArgs<int> e)
        {
            Debug.Print($"Item Added: {e.Item}");
        }

        public void ItemRemovedWatcher(object sender, ItemEventArgs<int> e)
        {
            Debug.Print($"Item Removed: {e.Item}");
        }

        public void WatchedListClearing(object sender, EventArgs args)
        {
            Debug.Print("List is clearing....");
        }

        public void WatchedListCleared(object sender, EventArgs args)
        {
            Debug.Print("List is now cleared!");
        }

    }

    // Ignore this extension method, it is just to help print out the contents of the bag to the Output console window in visual studio
    public static class BagExtensions
    {
        public static void DebugPrint<T>(this Bag<T> bag)
        {
            // exit early if bag has no content
            if (bag == null || bag.IsEmpty || bag.Count == 0) 
                return;

            // Use join to "loop" over the contents, concatenating the items together with commas between
            string row = '[' + string.Join(",", bag.Select(item => item.ToString())) + ']';

            // print out the result to the output window as a single row
            Debug.Print(row);
        }
    }


    // Class to use as an example in KeyedCollection
    public class MyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public MyEntity() { }

        public MyEntity(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

    public class Enemy
    {
        public int Health { get; set; }
        public float Position { get; set; }

        public Enemy() { }

        public void Reset()
        {
            Health = 0;
            Position = 0;
        }
    }

    class EnemyPoolable : IPoolable
    {
        // Example attributes
        public int Health { get; set; }
        public float Position { get; set; }

        // ... Your other Enemy class code here

        // IPoolable interface methods/attributes implemented below
        private Action<IPoolable> _returnAction;

        void IPoolable.Initialize(Action<IPoolable> returnAction)
        {
            // copy the instance reference of the return function so we can call it later
            _returnAction = returnAction;
        }

        public void Return()
        {
            // Reset your classes attributes here
            Health = 0;
            Position = 0;

            // check if this instance has already been returned
            if (_returnAction != null)
            {
                // not yet returned, return it now
                _returnAction.Invoke(this);
                // set the delegate instance reference to null, so we don't accidentally return it again
                _returnAction = null;
            }
        }

        public IPoolable NextNode { get; set; }
        public IPoolable PreviousNode { get; set; }
    }

}
