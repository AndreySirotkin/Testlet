using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Testlet.Service;

namespace Testlet.UnitTests.Service
{
    [TestFixture]
    public class Testlet_Randomize
    {
        private TestletSevice _testlet;
        #region items
        private List<Item> _items = new List<Item>() {
                new Item {
                    ItemId = "01",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "02",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "03",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "04",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "05",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "06",
                    ItemType = ItemTypeEnum.Operational,
                },
                new Item {
                    ItemId = "07",
                    ItemType = ItemTypeEnum.Pretest,
                },
                new Item {
                    ItemId = "08",
                    ItemType = ItemTypeEnum.Pretest,
                },
                new Item {
                    ItemId = "09",
                    ItemType = ItemTypeEnum.Pretest,
                },
                new Item {
                    ItemId = "10",
                    ItemType = ItemTypeEnum.Pretest,
                },
            };
        #endregion

        [SetUp]
        public void SetUp()
        {
            _testlet = new TestletSevice("id", _items);
        }

        [Test]
        public void Is_All_Items()
        {
            var result = _testlet.Randomize();
            var ids = string.Join("", result.OrderBy(i => i.ItemId).Select(i => i.ItemId));
            var origIds = string.Join("", _items.Select(i => i.ItemId));
            Assert.AreEqual(origIds, ids, "Not all items returned");
        }

        [Test]
        public void Is_Length_Same()
        {
            var result = _testlet.Randomize();
            Assert.AreEqual(_items.Count, result.Count, "Result length is wrong");
        }

        [Test]
        public void Is_Items_Unique()
        {
            var result = _testlet.Randomize();
            var hash = new HashSet<string>();
            foreach(var item in result) 
            {
                if (hash.Contains(item.ItemId))
                {
                    Assert.Fail("Not unique result");       
                }
                hash.Add(item.ItemId);
            }
        }

        [Test]
        public void Is_First_Two_Items_Pretest()
        {
            var result = _testlet.Randomize();
            Assert.AreEqual(ItemTypeEnum.Pretest, result[0].ItemType, "First should be Pretest");
            Assert.AreEqual(ItemTypeEnum.Pretest, result[1].ItemType, "Second should be Pretest");
        }

        [Test]
        public void Is_Random()
        {
            var result1 = _testlet.Randomize();
            var result2 = _testlet.Randomize();
            var ids1 = string.Join("", result1.Select(i => i.ItemId));
            var ids2 = string.Join("", result2.Select(i => i.ItemId));

            Assert.AreNotEqual(ids1, ids2, "Not randomized");
        }
    }
}
