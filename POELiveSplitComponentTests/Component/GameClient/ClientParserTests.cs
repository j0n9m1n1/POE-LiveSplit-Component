using Microsoft.VisualStudio.TestTools.UnitTesting;
using POELiveSplitComponent.Component;
using POELiveSplitComponent.Component.GameClient;
using System;

namespace POELiveSplitComponentTests.Component.GameClient
{
    [TestClass()]
    public class ClientParserTests
    {
        class MockClientEventHandler : IClientEventHandler
        {
            protected bool handledEvent = false;

            protected long expectedTimestamp;

            public MockClientEventHandler(long expectedTimestamp)
            {
                this.expectedTimestamp = expectedTimestamp;
            }

            public void AssertEventProcessed()
            {
                Assert.IsTrue(handledEvent);
            }

            public virtual void HandleLevelUp(long timestamp, int level)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleLoadEnd(long timestamp, string zoneName)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleLoadStart(long timestamp)
            {
                throw new NotImplementedException();
            }

            public virtual void HandleIzaroDialogue(long timestamp, string dialogue)
            {
                throw new NotImplementedException();
            }
        }

        class ExpectedLoadStart : MockClientEventHandler
        {
            public ExpectedLoadStart(long expectedTimestamp) : base(expectedTimestamp)
            { }

            public override void HandleLoadStart(long timestamp)
            {
                Assert.AreEqual(expectedTimestamp, timestamp);
                handledEvent = true;
            }
        }

        class ExpectedLoadEnd : MockClientEventHandler
        {
            private string expectedZoneName;

            public ExpectedLoadEnd(long expectedTimestamp, string expectedZoneName) : base(expectedTimestamp)
            {
                this.expectedZoneName = expectedZoneName;
            }

            public override void HandleLoadEnd(long timestamp, string zoneName)
            {
                Assert.AreEqual(expectedTimestamp, timestamp);
                Assert.AreEqual(expectedZoneName, zoneName);
                handledEvent = true;
            }
        }

        class ExpectedLevelUp : MockClientEventHandler
        {
            private int expectedLevel;

            public ExpectedLevelUp(long expectedTimestamp, int expectedLevel) : base(expectedTimestamp)
            {
                this.expectedLevel = expectedLevel;
            }

            public override void HandleLevelUp(long timestamp, int level)
            {
                Assert.AreEqual(expectedTimestamp, timestamp);
                Assert.AreEqual(expectedLevel, level);
                handledEvent = true;
            }
        }

        class ExpectedIzaroDialogue : MockClientEventHandler
        {
            private string expectedDialogue;

            public ExpectedIzaroDialogue(long expectedTimestamp, string expectedDialogue) : base(expectedTimestamp)
            {
                this.expectedDialogue = expectedDialogue;
            }

            public override void HandleIzaroDialogue(long timestamp, string dialogue)
            {
                Assert.AreEqual(expectedTimestamp, timestamp);
                Assert.AreEqual(expectedDialogue, dialogue);
                handledEvent = true;
            }
        }

        [TestMethod()]
        public void ProcessEnterZoneStart()
        {
            ExpectedLoadStart expected = new ExpectedLoadStart(177471343);
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2019/03/15 19:47:16 177471343 ccb [DEBUG Client 784] Got Instance Details from login server");
            expected.AssertEventProcessed();
        }

        [TestMethod()]
        public void ProcessEnterZoneEnd()
        {
            ExpectedLoadEnd expected = new ExpectedLoadEnd(177482140, "황혼의 해안");
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2023/12/17 03:47:23 177482140 cff94598 [INFO Client 24000] : 황혼의 해안에 진입했습니다.");
            expected.AssertEventProcessed();
        }

        [TestMethod()]
        public void ProcessLevelUp()
        {
            ExpectedLevelUp expected = new ExpectedLevelUp(177561171, 97);
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2023/12/16 16:04:54 177561171 cff94598 [INFO Client 1764] : zizizizizz(패스파인더) 님이 97레벨이 되었습니다.");
            expected.AssertEventProcessed();
        }

        [TestMethod()]
        public void ProcessIzaroDialogue()
        {
            ExpectedIzaroDialogue expected = new ExpectedIzaroDialogue(32090968, "빛나는 던전에서 기뻐하라, 어센던트.");
            ClientParser parser = new ClientParser(expected);
            parser.ProcessLine("2024/01/14 04:23:39 32090968 cff94598 [INFO Client 8548] 이자로: 빛나는 던전에서 기뻐하라, 어센던트.");
            expected.AssertEventProcessed();
        }
    }
}