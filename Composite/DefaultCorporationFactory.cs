using System;
using Composite.Interfaces;

namespace Composite {
    public class DefaultCorpoationFactory : ICorporationFactory {
        public Corporation Create(){
            var corporation = new Corporation("My huge Book Store Company!");
            corporation.Add(CreateWestStore());

            return corporation;
        }
        private IComponent CreateWestStore(){
            var store = new Store("West Store");
            store.Add(CreateFictionSection());

            return store;
        }
        private IComponent CreateFictionSection(){
            var section = new Section("Fiction");
            section.Add(new Book("Some alien cowboy"));
            section.Add(CreateScienceFictionSection());

            return section;
        }
        private IComponent CreateScienceFictionSection(){
            var section = new Section("Science Fiction");
            section.Add(new Book("Some wierd adventure in space"));
            section.Add(new Set("Star Wars", new Set("Prequel trilogy",
                 new Book("Episode I: The Phantom Menace"),
                 new Book("Episode II: Attack of the Clones"),
                 new Book("Episode III: Revenge of the Sith"),
                 new Set(
                     "Original trology",
                     new Book("Episode IV: A New Hope"),
                     new Book("Episode V: The Empire Strikes Back"),
                     new Book("Episode VI: Return of the Jedi")
                 ),
                 new Set(
                     "Sequel trilogy",
                     new Book("Episode VII: THe Force Awakens"),
                     new Book("Episode VIII: The Last Jedi"),
                     new Book("Episode IX: The Rise of the SkyWalker")
                 ))));

                 return section;
        }
    }
}
