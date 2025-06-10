using System.Collections.Generic;

namespace POELiveSplitComponent.Component.Timer
{
    public class Zone : IZone
    {
        public enum IconType { Wp, NoWp, Town }

        public static readonly List<Zone> ZONES;

        private static readonly Dictionary<Zone, Zone> REQUIRED;

        public static readonly Dictionary<IZone, IconType> ICONTYPES;

        static Zone()
        {
            ZONES = new List<Zone>();
            REQUIRED = new Dictionary<Zone, Zone>();
            ICONTYPES = new Dictionary<IZone, IconType>();

            Zone kitavaPart1Kill = new Zone("대성당 옥상", 1);
            Zone act6Home = new Zone("라이온아이 초소", 2);
            Zone act7Home = new Zone("다리 야영지", 2);
            Zone sarnRamparts = new Zone("사안 성벽", 2);
            Zone act8Home = new Zone("사안 야영지", 2);
            Zone bloodAqueduct = new Zone("피의 수로", 2);
            Zone act9Home = new Zone("하이게이트", 2);
            Zone act10Home = new Zone("오리아스 부두", 2);

            add(new Zone("라이온아이 초소", 1), IconType.Town);
            add(new Zone("해안 지대", 1), IconType.Wp);
            add(new Zone("갯벌", 1), IconType.NoWp);
            add(new Zone("물에 잠긴 길", 1), IconType.Wp);
            add(new Zone("바위 턱", 1), IconType.Wp);
            add(new Zone("고개", 1), IconType.Wp);
            add(new Zone("수용소 하층", 1), IconType.Wp);
            add(new Zone("수용소 상층", 1), IconType.NoWp);
            add(new Zone("죄수의 문", 1), IconType.Wp);
            add(new Zone("배들의 묘지", 1), IconType.Wp);
            add(new Zone("진노의 암굴", 1), IconType.Wp);
            add(new Zone("분노의 암굴", 1), IconType.NoWp);
            add(new Zone("남쪽 숲", 1), IconType.Wp);
            add(new Zone("숲 야영지", 1), IconType.Town);
            add(new Zone("강변길", 1), IconType.Wp);
            add(new Zone("서쪽 숲", 1), IconType.Wp);
            add(new Zone("버려진 경작지", 1), IconType.NoWp);
            add(new Zone("갈림길", 1), IconType.Wp);
            add(new Zone("죄악의 방 1층", 1), IconType.Wp);
            add(new Zone("죄악의 방 2층", 1), IconType.NoWp);
            add(new Zone("몰락한 성소 유적", 1), IconType.NoWp);
            add(new Zone("부서진 다리", 1), IconType.Wp);
            add(new Zone("습지대", 1), IconType.Wp);
            add(new Zone("바알 유적", 1), IconType.NoWp);
            add(new Zone("북쪽 숲", 1), IconType.Wp);
            add(new Zone("암굴", 1), IconType.Wp);
            add(new Zone("고대 피라미드", 1), IconType.NoWp);
            add(new Zone("사안 시내", 1), IconType.Wp);
            add(new Zone("사안 야영지", 1), IconType.Town);
            add(new Zone("빈민가", 1), IconType.NoWp);
            add(new Zone("화장터", 1), IconType.Wp);
            add(new Zone("하수도", 1), IconType.Wp);
            add(new Zone("장터", 1), IconType.Wp);
            add(new Zone("전쟁터", 1), IconType.Wp);
            add(new Zone("항구", 1), IconType.Wp);
            add(new Zone("솔라리스 사원 1층", 1), IconType.Wp);
            add(new Zone("솔라리스 사원 2층", 1), IconType.Wp);
            add(new Zone("칠흑의 군단 주둔지", 1), IconType.Wp);
            add(new Zone("루나리스 사원 1층", 1), IconType.Wp);
            add(new Zone("루나리스 사원 2층", 1), IconType.NoWp);
            add(new Zone("황실 정원", 1), IconType.Wp);
            add(new Zone("신의 셉터", 1), IconType.Wp);
            add(new Zone("신의 셉터 상층", 1), IconType.NoWp);
            add(new Zone("수로", 1), IconType.Wp);
            add(new Zone("하이게이트", 1), IconType.Town);
            add(new Zone("말라붙은 호수", 1), IconType.NoWp);
            add(new Zone("광산 1층", 1), IconType.NoWp);
            add(new Zone("광산 2층", 1), IconType.NoWp);
            add(new Zone("수정 광맥", 1), IconType.Wp);
            add(new Zone("다레소의 꿈", 1), IconType.NoWp);
            add(new Zone("카옴의 꿈", 1), IconType.NoWp);
            add(new Zone("카옴의 요새", 1), IconType.Wp);
            add(new Zone("대 투기장", 1), IconType.Wp);
            add(new Zone("짐승의 소굴 1층", 1), IconType.NoWp);
            add(new Zone("짐승의 소굴 2층", 1), IconType.NoWp);
            add(new Zone("수확소", 1), IconType.Wp);
            add(new Zone("오르막길", 1), IconType.NoWp);
            add(new Zone("노예 감호소", 1), IconType.Wp);
            add(new Zone("감시탑", 1), IconType.Town);
            add(new Zone("관리 구역", 1), IconType.NoWp);
            add(new Zone("오리아스 광장", 1), IconType.Wp);
            add(new Zone("템플러의 법정", 1), IconType.Wp);
            add(new Zone("결백의 방", 1), IconType.Wp);
            add(new Zone("타오르는 법정", 1), IconType.NoWp);
            add(new Zone("멸망한 광장", 1), IconType.Wp);
            add(new Zone("납골당", 1), IconType.NoWp);
            add(kitavaPart1Kill, IconType.Wp);
            add(act6Home, IconType.Town, kitavaPart1Kill);
            add(new Zone("해안 지대", 2), IconType.Wp, act6Home);
            add(new Zone("갯벌", 2), IconType.NoWp, act6Home);
            add(new Zone("카루이 요새", 2), IconType.NoWp, act6Home);
            add(new Zone("산등성이", 2), IconType.Wp, act6Home);
            add(new Zone("수용소 하층", 2), IconType.Wp, act6Home);
            add(new Zone("샤브론의 탑", 2), IconType.NoWp, act6Home);
            add(new Zone("죄수의 문", 2), IconType.Wp, act6Home);
            add(new Zone("서쪽 숲", 2), IconType.Wp, act6Home);
            add(new Zone("강변길", 2), IconType.Wp, act6Home);
            add(new Zone("남쪽 숲", 2), IconType.Wp, act6Home);
            add(new Zone("분노의 암굴", 2), IconType.NoWp, act6Home);
            add(new Zone("등대", 2), IconType.Wp, act6Home);
            add(new Zone("염수왕의 암초", 2), IconType.Wp, act6Home);
            add(act7Home, IconType.Town);
            add(new Zone("부서진 다리", 2), IconType.NoWp, act7Home);
            add(new Zone("갈림길", 2), IconType.Wp, act7Home);
            add(new Zone("몰락한 성소 유적", 2), IconType.NoWp, act7Home);
            add(new Zone("지하실", 2), IconType.Wp, act7Home);
            add(new Zone("죄악의 방 1층", 2), IconType.Wp, act7Home);
            add(new Zone("말리가로의 지성소", 2), IconType.NoWp, act7Home);
            add(new Zone("죄악의 방 2층", 2), IconType.NoWp, act7Home);
            add(new Zone("굴", 2), IconType.Wp, act7Home);
            add(new Zone("잿빛 들판", 2), IconType.Wp, act7Home);
            add(new Zone("북쪽 숲", 2), IconType.Wp, act7Home);
            add(new Zone("둑길", 2), IconType.Wp, act7Home);
            add(new Zone("바알 도시", 2), IconType.Wp, act7Home);
            add(new Zone("부패의 사원 1층", 2), IconType.NoWp, act7Home);
            add(new Zone("부패의 사원 2층", 2), IconType.NoWp, act7Home);
            add(sarnRamparts, IconType.Wp);
            add(act8Home, IconType.Town, sarnRamparts);
            add(new Zone("독성 도관", 2), IconType.NoWp, act8Home);
            add(new Zone("도이드리의 정화조", 2), IconType.Wp, act8Home);
            add(new Zone("부두", 2), IconType.NoWp, act8Home);
            add(new Zone("곡물의 문", 2), IconType.Wp, act8Home);
            add(new Zone("황실 정원", 2), IconType.Wp, act8Home);
            add(new Zone("솔라리스 사원 1층", 2), IconType.Wp, act8Home);
            add(new Zone("솔라리스 사원 2층", 2), IconType.NoWp, act8Home);
            add(new Zone("목욕탕", 2), IconType.Wp, act8Home);
            add(new Zone("루나리스 중앙 광장", 2), IconType.Wp, act8Home);
            add(new Zone("루나리스 사원 1층", 2), IconType.Wp, act8Home);
            add(new Zone("루나리스 사원 2층", 2), IconType.NoWp, act8Home);
            add(new Zone("항구 다리", 2), IconType.NoWp, act8Home);
            add(bloodAqueduct, IconType.Wp);
            add(act9Home, IconType.Town, bloodAqueduct);
            add(new Zone("비탈", 2), IconType.NoWp, act9Home);
            add(new Zone("바스티리 사막", 2), IconType.Wp, act9Home);
            add(new Zone("구릉", 2), IconType.Wp, act9Home);
            add(new Zone("터널", 2), IconType.Wp, act9Home);
            add(new Zone("끓어오르는 호수", 2), IconType.NoWp, act9Home);
            add(new Zone("채석장", 2), IconType.Wp, act9Home);
            add(new Zone("재련소", 2), IconType.NoWp, act9Home);
            add(new Zone("짐승의 소굴", 2), IconType.NoWp, act9Home);
            add(new Zone("썩어가는 중심부", 2), IconType.NoWp, act9Home);
            add(act10Home, IconType.Town);
            add(new Zone("대성당 옥상", 2), IconType.NoWp, act10Home);
            add(new Zone("파괴된 광장", 2), IconType.Wp, act10Home);
            add(new Zone("타오르는 법정", 2), IconType.NoWp, act10Home);
            add(new Zone("운하", 2), IconType.NoWp, act10Home);
            add(new Zone("먹이통", 2), IconType.NoWp, act10Home);
            add(new Zone("카루이의 바닷가", 2), IconType.Town, act10Home);
        }

        private static void add(Zone zone, IconType icon, Zone requirement = null)
        {
            ZONES.Add(zone);
            ICONTYPES[zone] = icon;
            REQUIRED[zone] = requirement;
        }

        // Creates a zone representation using the zone name.
        // Tries to guess the part using information about the encountered zones so far.
        public static IZone Parse(string zoneName, HashSet<IZone> encounteredZones)
        {
            return new Zone(zoneName, guessPart(zoneName, encounteredZones));
        }

        // Returns part 1 if the requirements for part 2 are not met. 2 otherwise.
        // Assuming the run started in act 1, if the previous zone requirements are not met for the part 2 equivalent, then we are certain that this is a part 1 zone.
        // Assuming runners don't go back to part 1 zones once in part 2, we are quite certain that this is a part 2 zone.
        // They may return to part 1 towns but returning part 2 instead would not cause more or fewer splits to occur.
        private static int guessPart(string zoneName, HashSet<IZone> encounteredZones)
        {
            Zone zone = new Zone(zoneName, 2);
            // Check to make sure that a part 2 version exists for this zone.
            if (REQUIRED.ContainsKey(zone))
            {
                Zone required = REQUIRED[zone];
                if (required == null || encounteredZones.Contains(required))
                {
                    return 2;
                }
            }
            return 1;
        }

        private string name;
        private int part;

        private Zone(string name, int part)
        {
            this.name = name;
            this.part = part;
        }

        public string Serialize()
        {
            return ToString();
        }

        public string SplitName()
        {
            if (part == 1)
            {
                return name;
            }
            return ToString();
        }

        public override string ToString()
        {
            return name + " (Part " + part + ")";
        }

        public override bool Equals(object obj)
        {
            Zone zone = obj as Zone;
            if (zone == null)
            {
                return false;
            }
            return name.Equals(zone.name) && part == zone.part;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() * 23 + part;
        }
    }
}
