using LiveSplit.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LiveSplit.Model.Input;
using System.Runtime.Serialization;
using POELiveSplitComponent.Component.Settings;
using POELiveSplitComponent.Component.Timer;

namespace POELiveSplitComponentTests.Component.Timer
{
    [TestClass()]
    public class LoadRemoverSplitterTests
    {
        class MockTimerModel : ITimerModel
        {
            public int NumSplits = 0;
            public int NumStarts = 0;

            public void Split()
            {
                NumSplits++;
            }

            public void Start()
            {
                NumStarts++;
            }

            public LiveSplitState CurrentState
            {
                get
                {
                    return (LiveSplitState)FormatterServices.GetUninitializedObject(typeof(LiveSplitState));
                }

                set
                {
                    throw new NotImplementedException();
                }
            }

            public event EventHandler OnPause;
            public event EventHandlerT<TimerPhase> OnReset;
            public event EventHandler OnResume;
            public event EventHandler OnScrollDown;
            public event EventHandler OnScrollUp;
            public event EventHandler OnSkipSplit;
            public event EventHandler OnSplit;
            public event EventHandler OnStart;
            public event EventHandler OnSwitchComparisonNext;
            public event EventHandler OnSwitchComparisonPrevious;
            public event EventHandler OnUndoAllPauses;
            public event EventHandler OnUndoSplit;

            public void InitializeGameTime()
            {
                throw new NotImplementedException();
            }

            public void Pause()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                NumSplits = 0;
                NumStarts = 0;
            }

            public void Reset(bool updateSplits)
            {
                throw new NotImplementedException();
            }

            public void ResetAndSetAttemptAsPB()
            {
                throw new NotImplementedException();
            }

            public void ScrollDown()
            {
                throw new NotImplementedException();
            }

            public void ScrollUp()
            {
                throw new NotImplementedException();
            }

            public void SkipSplit()
            {
                throw new NotImplementedException();
            }

            public void SwitchComparisonNext()
            {
                throw new NotImplementedException();
            }

            public void SwitchComparisonPrevious()
            {
                throw new NotImplementedException();
            }

            public void UndoAllPauses()
            {
                throw new NotImplementedException();
            }

            public void UndoSplit()
            {
                throw new NotImplementedException();
            }
        }

        private static readonly Zone LIONEYES1 = Zone.ZONES.Find(z => z.Serialize().Equals("라이온아이 초소 (Part 1)"));
        private static readonly Zone LIONEYES2 = Zone.ZONES.Find(z => z.Serialize().Equals("라이온아이 초소 (Part 2)"));

        [TestMethod()]
        public void HandleSplitZonesTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.SplitZones.Add(LIONEYES1);
            settings.SplitLevels.Add(2);
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            splitter.HandleLoadStart(1);
            splitter.HandleLoadEnd(2, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
            // Wrong zone.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "해안 지대");
            Assert.AreEqual(1, timer.NumSplits);
            // Not splitting on levels.
            splitter.HandleLevelUp(5, 2);
            Assert.AreEqual(1, timer.NumSplits);
            // Already entered this zone.
            splitter.HandleLoadStart(6);
            splitter.HandleLoadEnd(7, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
        }

        [TestMethod()]
        public void HandleLevelUpTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Levels;
            settings.SplitZones.Add(LIONEYES1);
            settings.SplitLevels.Add(10);
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Not splitting on this level.
            splitter.HandleLevelUp(1, 9);
            Assert.AreEqual(0, timer.NumSplits);
            splitter.HandleLevelUp(2, 10);
            Assert.AreEqual(1, timer.NumSplits);
            // Already reached this level.
            splitter.HandleLevelUp(3, 10);
            Assert.AreEqual(1, timer.NumSplits);
            // Not splitting on zones.
            splitter.HandleLoadStart(4);
            splitter.HandleLoadEnd(5, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
        }

        [TestMethod()]
        public void HandleSplitZonesPlusLevelTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.SplitZones.Add(LIONEYES1);
            settings.SplitZoneLevels.Add(70);
            settings.SplitLevels.Add(2);
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Split on zone.
            splitter.HandleLoadStart(1);
            splitter.HandleLoadEnd(2, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
            // Not splitting on normal level settings.
            splitter.HandleLevelUp(3, 2);
            Assert.AreEqual(1, timer.NumSplits);
            // But should split on levels added through the zone settings.
            splitter.HandleLevelUp(3, 70);
            Assert.AreEqual(2, timer.NumSplits);
        }

        [TestMethod()]
        public void HandleAutosplitDisabledTest()
        {
            foreach (ComponentSettings.SplitCriteria c in Enum.GetValues(typeof(ComponentSettings.SplitCriteria)))
            {
                ComponentSettings settings = new ComponentSettings();
                settings.CriteriaToSplit = c;
                settings.SplitZones.Add(LIONEYES1);
                settings.SplitZoneLevels.Add(70);
                settings.SplitLevels.Add(10);
                settings.AutoSplitEnabled = false;
                MockTimerModel timer = new MockTimerModel();

                LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
                // No splits are performed.
                splitter.HandleLevelUp(1, 10);
                splitter.HandleLevelUp(2, 70);
                splitter.HandleLoadStart(3);
                splitter.HandleLoadEnd(4, "라이온아이 초소");
                Assert.AreEqual(0, timer.NumSplits);
            }
        }

        [TestMethod()]
        public void NoSplitWhenInLabModeTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            settings.SplitLevels.Add(10);
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Lab mode disables other autosplitting behaviour.
            splitter.HandleLevelUp(1, 10);
            Assert.AreEqual(0, timer.NumSplits);
        }

        [TestMethod()]
        public void Part1Part2Test()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.SplitZones.Add(LIONEYES1);
            settings.SplitZones.Add(LIONEYES2);
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            splitter.HandleLoadStart(1);
            splitter.HandleLoadEnd(2, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
            // Already entered and still considered part 1.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "라이온아이 초소");
            Assert.AreEqual(1, timer.NumSplits);
            // Enter the prerequisite zone.
            splitter.HandleLoadStart(5);
            splitter.HandleLoadEnd(6, "대성당 옥상");
            Assert.AreEqual(1, timer.NumSplits);
            // Now considered to be entering part 2.
            splitter.HandleLoadStart(7);
            splitter.HandleLoadEnd(8, "라이온아이 초소");
            Assert.AreEqual(2, timer.NumSplits);
        }

        [TestMethod()]
        public void LabAllZonesTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            splitter.HandleIzaroDialogue(1, "...");
            Assert.AreEqual(0, timer.NumStarts);
            // Enter Aspirants' Plaza.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "지망자의 광장");
            Assert.AreEqual(0, timer.NumSplits);
            // Intro Izaro dialogue.
            splitter.HandleIzaroDialogue(5, "너는 군주의 미궁 입구에 서 있다. 이 안쪽은 정의의 여신께서 주재하시는 영역이니라. 여신께서는 한 손에는 너의 정신을, 다른 손에는 너의 심장을 올려 무게를 가늠하실 것이다. 네가 부족하다면 사형이 선고되리라. 네가 가치 있다면 제국의 영광과 사랑이 주어지리라. 군주의 미궁이 너를 기다린다. 현명하게 선택하고 빠르게 행동하며 완벽하게 신뢰하라. 그리하면 끝에 다다르게 될지도 모르니.");
            Assert.AreEqual(0, timer.NumStarts);
            // Activated lab device.
            splitter.HandleIzaroDialogue(6, "정의가 승리하리라.");
            Assert.AreEqual(1, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
            // Run through some zones.
            splitter.HandleLoadStart(7);
            splitter.HandleLoadEnd(8, "...");
            Assert.AreEqual(1, timer.NumSplits);
            splitter.HandleLoadStart(8);
            splitter.HandleLoadEnd(9, "...");
            Assert.AreEqual(2, timer.NumSplits);
            // Izaro death
            splitter.HandleIzaroDialogue(10, "나는 제국을 위하여 죽노라!");
            Assert.AreEqual(3, timer.NumSplits);
        }

        [TestMethod()]
        public void LabOnlyTrialsTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Enter Aspirants' Plaza.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "지망자의 광장");
            // Activated lab device.
            splitter.HandleIzaroDialogue(6, "정의가 승리하리라.");
            Assert.AreEqual(1, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
            // Run through an intermediate lab zone.
            splitter.HandleLoadStart(7);
            splitter.HandleLoadEnd(8, "...");
            Assert.AreEqual(0, timer.NumSplits);
            // Arrive at Aspirant's Trial.
            splitter.HandleLoadStart(8);
            splitter.HandleLoadEnd(9, "지망자의 시험");
            Assert.AreEqual(1, timer.NumSplits);
            splitter.HandleLoadStart(10);
            splitter.HandleLoadEnd(11, "지망자의 시험");
            Assert.AreEqual(2, timer.NumSplits);
            // Izaro death
            splitter.HandleIzaroDialogue(12, "너는 자유로다!");
            Assert.AreEqual(3, timer.NumSplits);
        }

        [TestMethod()]
        public void LabOnlyPlazaReset()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Enter Aspirants' Plaza.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "지망자의 광장");
            // Activated lab device.
            splitter.HandleIzaroDialogue(6, "정의가 승리하리라.");
            Assert.AreEqual(1, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
            // Run through an intermediate lab zone.
            splitter.HandleLoadStart(7);
            splitter.HandleLoadEnd(8, "...");
            Assert.AreEqual(0, timer.NumSplits);
            // Enter Aspirants' Plaza resets timer
            splitter.HandleLoadStart(9);
            splitter.HandleLoadEnd(10, "지망자의 광장");
            Assert.AreEqual(0, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
        }

        [TestMethod()]
        public void LabOnlyPlazaResetAllZones()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            settings.LabSplitType = ComponentSettings.LabSplitMode.AllZones;
            MockTimerModel timer = new MockTimerModel();

            LoadRemoverSplitter splitter = new LoadRemoverSplitter(timer, settings);
            // Enter Aspirants' Plaza.
            splitter.HandleLoadStart(3);
            splitter.HandleLoadEnd(4, "지망자의 광장");
            // Activated lab device.
            splitter.HandleIzaroDialogue(6, "정의가 승리하리라.");
            Assert.AreEqual(1, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
            // Run through an intermediate lab zone.
            splitter.HandleLoadStart(7);
            splitter.HandleLoadEnd(8, "...");
            Assert.AreEqual(1, timer.NumSplits);
            // Enter Aspirants' Plaza resets timer
            splitter.HandleLoadStart(9);
            splitter.HandleLoadEnd(10, "지망자의 광장");
            Assert.AreEqual(0, timer.NumStarts);
            Assert.AreEqual(0, timer.NumSplits);
        }
    }
}
