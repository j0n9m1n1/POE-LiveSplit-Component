using Microsoft.VisualStudio.TestTools.UnitTesting;
using POELiveSplitComponent.Component.Timer;
using System.Collections.Generic;

namespace POELiveSplitComponentTests.Component.Timer
{
    [TestClass()]
    public class ZoneTests
    {
        private static readonly IZone TEST_ZONE_1 = Zone.Parse("라이온아이 초소", new HashSet<IZone>());

        private static readonly IZone PREREQ = Zone.Parse("대성당 옥상", new HashSet<IZone>());

        private static readonly IZone TEST_ZONE_2 = Zone.Parse("라이온아이 초소", new HashSet<IZone>() { PREREQ });

        [TestMethod()]
        public void SerializeTest()
        {
            Assert.AreEqual("라이온아이 초소 (Part 1)", TEST_ZONE_1.Serialize());
            Assert.AreEqual("라이온아이 초소 (Part 2)", TEST_ZONE_2.Serialize());
        }

        [TestMethod()]
        public void SplitNameTest()
        {
            Assert.AreEqual("라이온아이 초소", TEST_ZONE_1.SplitName());
            Assert.AreEqual("라이온아이 초소 (Part 2)", TEST_ZONE_2.SplitName());
        }

        [TestMethod()]
        public void EqualityTest()
        {
            Zone lioneyes1 = Zone.ZONES.Find(z => z.Serialize().Equals("라이온아이 초소 (Part 1)"));
            Zone lioneyes2 = Zone.ZONES.Find(z => z.Serialize().Equals("라이온아이 초소 (Part 2)"));
            Assert.AreEqual(lioneyes1, TEST_ZONE_1);
            Assert.AreEqual(lioneyes2, TEST_ZONE_2);
        }

        [TestMethod()]
        public void ParseNoPart2Test()
        {
            Assert.AreEqual("신의 셉터 (Part 1)", Zone.Parse("신의 셉터", new HashSet<IZone>()).Serialize());
        }

        [TestMethod()]
        public void ParseNoRequirementTest()
        {
            Assert.AreEqual("다리 야영지 (Part 2)", Zone.Parse("다리 야영지", new HashSet<IZone>()).Serialize());
        }

        [TestMethod()]
        public void ParseHasPrereqTest()
        {
            IZone bloodAqueduct = Zone.Parse("피의 수로", new HashSet<IZone>());
            Assert.AreEqual("하이게이트 (Part 2)", Zone.Parse("하이게이트", new HashSet<IZone>() { bloodAqueduct }).Serialize());
        }

        [TestMethod()]
        public void ParseMissingPrereqTest()
        {
            Assert.AreEqual("하이게이트 (Part 1)", Zone.Parse("하이게이트", new HashSet<IZone>()).Serialize());
        }

        [TestMethod()]
        public void ParseWrongPartPrereqTest()
        {
            Assert.AreEqual("해안 지대 (Part 1)", Zone.Parse("해안 지대", new HashSet<IZone>() { TEST_ZONE_1 }).Serialize());
        }

        [TestMethod()]
        public void ParseCorrectPartPrereqTest()
        {
            Assert.AreEqual("해안 지대 (Part 2)", Zone.Parse("해안 지대", new HashSet<IZone>() { TEST_ZONE_2 }).Serialize());
        }

        [TestMethod()]
        public void IconTest()
        {
            Assert.AreEqual(Zone.IconType.Town, Zone.ICONTYPES[TEST_ZONE_1]);
            Assert.AreEqual(Zone.IconType.Wp, Zone.ICONTYPES[Zone.Parse("해안 지대", new HashSet<IZone>())]);
            Assert.AreEqual(Zone.IconType.NoWp, Zone.ICONTYPES[Zone.Parse("물결 섬", new HashSet<IZone>())]);
        }
    }
}