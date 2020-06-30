using System;
using System.Linq;
using System.Collections.Generic;

namespace Testlet.Service
{
    public class TestletSevice
    {
        public string TestletId;
        private List<Item> Items;

        public TestletSevice(string testletId, List<Item> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (items.Count != 10)
            {
                throw new ArgumentException("10 items expected");
            }
            if (items.Where(i => i.ItemType == ItemTypeEnum.Pretest).Count() != 4)
            {
                throw new ArgumentException("4 Pretest items expected");
            }
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            var rnd = new Random();
            
            //Take first two pretest
            var result = Items.Where(i => i.ItemType == ItemTypeEnum.Pretest)
                .OrderBy(i => rnd.Next())
                .Take(2)
                .ToList();

            //Add other items
            var rest = Items.Except(result)
                .OrderBy(i => rnd.Next());

            result.AddRange(rest);

            return result;
        }
    }


}