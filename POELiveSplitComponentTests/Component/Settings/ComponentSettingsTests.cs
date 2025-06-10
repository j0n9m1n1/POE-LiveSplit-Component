﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using POELiveSplitComponent.Component.Settings;
using POELiveSplitComponent.Component.Timer;
using System.Collections.Generic;
using System.Xml;

namespace POELiveSplitComponentTests.Component.Settings
{
    [TestClass()]
    public class ComponentSettingsTests
    {
        [TestMethod()]
        public void SetDefaultSettingsTest()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<AutoSplitterSettings></AutoSplitterSettings>");
            XmlNode nodeSettings = xml.FirstChild;
            ComponentSettings settings = new ComponentSettings();
            settings.SetSettings(nodeSettings);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsFalse(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.AllZones, settings.LabSplitType);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Zones, settings.CriteriaToSplit);
            Assert.AreEqual(0, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(0, settings.SplitLevels.Count);
        }

        [TestMethod()]
        public void SetSettingsBeforeLevelChangesTest()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(
@"<AutoSplitterSettings>
   <log.location>C:/Daum Games/Path of Exile/logs/KakaoClient.txt</log.location>
   <load.removal>True</load.removal>
   <auto.split>True</auto.split>
   <split.labyrinth>False</split.labyrinth>
   <split.zones.on>
      <split.zone>라이온아이 초소 (Part 1)</split.zone>
      <split.zone>오리아스 부두 (Part 2)</split.zone>
   </split.zones.on>
</AutoSplitterSettings>");
            XmlNode nodeSettings = xml.FirstChild;
            ComponentSettings settings = new ComponentSettings();
            settings.SetSettings(nodeSettings);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsTrue(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.AllZones, settings.LabSplitType);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Zones, settings.CriteriaToSplit);
            Assert.AreEqual(2, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(0, settings.SplitLevels.Count);
            Assert.AreEqual("C:/Daum Games/Path of Exile/logs/KakaoClient.txt", settings.LogLocation);
        }

        [TestMethod()]
        public void SetSettingsWithLevelChangesTest()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(
@"<AutoSplitterSettings>
   <log.location>C:/Daum Games/Path of Exile/logs/KakaoClient.txt</log.location>
   <load.removal>False</load.removal>
   <auto.split>True</auto.split>
   <split.labyrinth>False</split.labyrinth>
   <split.criteria>Levels</split.criteria>
   <split.zones.on>
      <split.zone>라이온아이 초소 (Part 1)</split.zone>
      <split.zone>갯벌 (Part 1)</split.zone>
      <split.zone>바위 턱 (Part 1)</split.zone>
      <split.level>70</split.level>
   </split.zones.on>
   <split.levels.on>
      <split.level>3</split.level>
   </split.levels.on>
</AutoSplitterSettings>");
            XmlNode nodeSettings = xml.FirstChild;
            ComponentSettings settings = new ComponentSettings();
            settings.SetSettings(nodeSettings);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsFalse(settings.LoadRemovalEnabled);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Levels, settings.CriteriaToSplit);
            Assert.AreEqual(3, settings.SplitZones.Count);
            Assert.IsTrue(new HashSet<int> { 70 }.SetEquals(settings.SplitZoneLevels));
            Assert.IsTrue(new HashSet<int> { 3 }.SetEquals(settings.SplitLevels));
            Assert.AreEqual(@"C:/Daum Games/Path of Exile/logs/KakaoClient.txt", settings.LogLocation);
        }

        [TestMethod()]
        public void SetSettingsWithLegacyLabTest()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(
@"<AutoSplitterSettings>
   <log.location>C:/Daum Games/Path of Exile/logs/KakaoClient.txt</log.location>
   <load.removal>False</load.removal>
   <auto.split>True</auto.split>
   <split.labyrinth>True</split.labyrinth>
   <split.criteria>Levels</split.criteria>
   <split.zones.on>
      <split.zone>라이온아이 초소 (Part 1)</split.zone>
      <split.zone>갯벌 (Part 1)</split.zone>
      <split.zone>바위 턱 (Part 1)</split.zone>
   </split.zones.on>
   <split.levels.on>
      <split.level>3</split.level>
   </split.levels.on>
</AutoSplitterSettings>");
            XmlNode nodeSettings = xml.FirstChild;
            ComponentSettings settings = new ComponentSettings();
            settings.SetSettings(nodeSettings);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsFalse(settings.LoadRemovalEnabled);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Labyrinth, settings.CriteriaToSplit);
            Assert.AreEqual(3, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(1, settings.SplitLevels.Count);
            Assert.AreEqual(@"C:/Daum Games/Path of Exile/logs/KakaoClient.txt", settings.LogLocation);
        }

        [TestMethod()]
        public void GetAndSetDefaultSettingsTest()
        {
            XmlDocument xml = new XmlDocument();
            ComponentSettings settings = new ComponentSettings();
            XmlNode node = settings.GetSettings(xml);

            settings = new ComponentSettings();
            settings.SetSettings(node);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsFalse(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.AllZones, settings.LabSplitType);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Zones, settings.CriteriaToSplit);
            Assert.AreEqual(0, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(0, settings.SplitLevels.Count);
        }

        [TestMethod()]
        public void GetAndSetCustomSettingsTest()
        {
            XmlDocument xml = new XmlDocument();
            ComponentSettings settings = new ComponentSettings();
            settings.AutoSplitEnabled = false;
            settings.LoadRemovalEnabled = true;
            settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            settings.GenerateWithIcons = false;
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Levels;
            settings.SplitZones.Add(Zone.ZONES[0]);
            settings.SplitZoneLevels.Add(70);
            settings.SplitLevels.Add(100);
            XmlNode node = settings.GetSettings(xml);

            settings = new ComponentSettings();
            settings.SetSettings(node);
            Assert.IsFalse(settings.AutoSplitEnabled);
            Assert.IsTrue(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.Trials, settings.LabSplitType);
            Assert.IsFalse(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Levels, settings.CriteriaToSplit);
            Assert.IsTrue(new HashSet<IZone> { Zone.ZONES[0] }.SetEquals(settings.SplitZones));
            Assert.IsTrue(new HashSet<int> { 70 }.SetEquals(settings.SplitZoneLevels));
            Assert.IsTrue(new HashSet<int> { 100 }.SetEquals(settings.SplitLevels));
        }

        [TestMethod()]
        public void CancelTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.AutoSplitEnabled = false;
            settings.LoadRemovalEnabled = true;
            settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            settings.GenerateWithIcons = false;
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Levels;
            settings.SplitZones.Add(Zone.ZONES[0]);
            settings.SplitZoneLevels.Add(70);
            settings.SplitLevels.Add(100);
            
            // Emulate a cancel.
            XmlDocument xml = new XmlDocument();
            settings.SetSettings(new ComponentSettings().GetSettings(xml));
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsFalse(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.AllZones, settings.LabSplitType);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Zones, settings.CriteriaToSplit);
            Assert.AreEqual(0, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(0, settings.SplitLevels.Count);
        }

        [TestMethod()]
        public void CancelMissingPropsTest()
        {
            ComponentSettings settings = new ComponentSettings();
            settings.AutoSplitEnabled = false;
            settings.LoadRemovalEnabled = true;
            settings.LabSplitType = ComponentSettings.LabSplitMode.Trials;
            settings.GenerateWithIcons = false;
            settings.CriteriaToSplit = ComponentSettings.SplitCriteria.Labyrinth;
            settings.SplitZones.Add(Zone.ZONES[0]);
            settings.SplitZoneLevels.Add(70);
            settings.SplitLevels.Add(100);

            // Emulate a cancel where the properties were fetched from file.
            // Never has been observed but we'll handle it anyways.
            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<AutoSplitterSettings><load.removal>True</load.removal></AutoSplitterSettings>");
            XmlNode nodeSettings = xml.FirstChild;
            settings.SetSettings(nodeSettings);
            Assert.IsTrue(settings.AutoSplitEnabled);
            Assert.IsTrue(settings.LoadRemovalEnabled);
            Assert.AreEqual(ComponentSettings.LabSplitMode.AllZones, settings.LabSplitType);
            Assert.IsTrue(settings.GenerateWithIcons);
            Assert.AreEqual(ComponentSettings.SplitCriteria.Zones, settings.CriteriaToSplit);
            Assert.AreEqual(0, settings.SplitZones.Count);
            Assert.AreEqual(0, settings.SplitZoneLevels.Count);
            Assert.AreEqual(0, settings.SplitLevels.Count);
        }
    }
}