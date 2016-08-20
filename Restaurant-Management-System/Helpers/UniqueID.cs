using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Chocolatey.Comparision;
namespace Chocolatey.Data.Common
{
    public class UniqueID
    {
        private static int _previousRandomNumber;
        private static List<int> _associatedIds;
        private static List<ICollection> _collections;
        private static List<int> _collectionsCounts;

        private static Random _randomNumber;

        private static void Initailize()
        {
            if (_associatedIds == null)
                _associatedIds = new List<int>();
            if (_collections == null)
                _collections = new List<ICollection>();
            if (_collectionsCounts == null)
                _collectionsCounts = new List<int>();
            if (_randomNumber == null)
                _randomNumber = new Random();
        }
        private static int GenerateCustomerId()
        {
            Initailize();



            if (_previousRandomNumber == 0)
                _previousRandomNumber = _randomNumber.Next(99);

            string collectionCount = string.Empty;

            if (collectionCount == string.Empty)
            {
                List<int> lastCounts = new List<int>();
                foreach (int count in _collectionsCounts)
                {
                    collectionCount += (count + 1).ToString();
                    lastCounts.Add(count + 1);
                }
                for (int i = 0; i < _collectionsCounts.Count; i++)
                    _collectionsCounts[i] = lastCounts[i];
            }

            int newCustomerId = int.Parse(collectionCount +
                (_associatedIds.Count % 10).ToString() + (_previousRandomNumber++).ToString());

            if (!IdExists(newCustomerId))
            {
                _associatedIds.Add(newCustomerId);
                return newCustomerId;
            }
            else return GenerateCustomerId();

        }
        public static bool IdExists(int customerId)
        {



            Initailize();
            bool duplicateIdInCollections = false;
            int listCount = (from int id in _associatedIds where id == customerId select id).Count();

            foreach (ICollection collection in _collections)
            {
                if ((from int id in collection where id == customerId select id).Count() == 0)
                    duplicateIdInCollections &= false;
                else duplicateIdInCollections &= true;

            }

            if (listCount == 0 && duplicateIdInCollections == false)
                return false;
            else return true;
        }
        public static int NextId(params ICollection[] collections)
        {
            Initailize();

            foreach (ICollection collection in collections)
            {
                if (!_collections.Contains(collection,new CollectionComparer()))
                {
                    _collections.Add(collection);
                    _collectionsCounts.Add(collection.Count);
                }
            }
            
            return GenerateCustomerId();
        }
        public static implicit operator int(UniqueID uniqueId)
        {
            return NextId();
        }

    }

}
