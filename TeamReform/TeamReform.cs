/// Copyright [2019] [https://github.com/nomrand]
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
/// 
///     http://www.apache.org/licenses/LICENSE-2.0
/// 
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform
{
    static public class TeamReform
    {
        public const int TRY_NUM = 10;

        /// <summary>
        /// Reform(convert) Team structure
        /// </summary>
        /// <param name="beforeMemberList">Target members list</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of beforeMemberList is before-Team's key</param>
        /// <param name="afterTeamNum">Number of after-Team to be devided</param>
        /// <returns>Reformed members list</returns>
        /// <example>
        /// Before:12 Teams, 4 people in each Team<br>
        /// After :16 Teams, 3 people in each Team<br>
        /// <code>
        /// // *** Prepare input data ***
        /// // All member in before-Team must be added in flat 
        /// List<List<string>> beforeMemberList = new List<List<string>>()
        /// {
        ///     // member from before-Team1 (Team key is index 1)
        ///     new string[] { "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
        ///     new string[] { "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
        ///     new string[] { "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
        ///     new string[] { "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
        ///     // member from before-Team2 (Team key is index 1)
        ///     new string[] { "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
        ///     ...
        ///     // member from before-Team12 (Team key is index 1)
        ///     new string[] { "MEMBER1fromT12", "TEAM12", "INFO12-1-1" }.ToList(),
        ///     new string[] { "MEMBER2fromT12", "TEAM12", "INFO12-2-1" }.ToList(),
        ///     new string[] { "MEMBER3fromT12", "TEAM12", "INFO12-3-1" }.ToList(),
        ///     new string[] { "MEMBER4fromT12", "TEAM12", "INFO12-4-1" }.ToList(),
        /// };
        /// 
        /// int afterTeamNum = 16;
        /// 
        /// // *** Call method ***
        /// // Reform "12 Team x 4 People" structure to "16 Team x 3 People" 
        /// List<List<string>> afterMemberList = ReformTeam(beforeMemberList, 1, afterTeamNum);
        /// 
        /// // *** Use result ***
        /// // afterMemberList consists of all member which has after-Team's name, in flat
        /// // Each after-Team's name is number string(like "0", "1", "2", ..., "16")
        /// List<string> member1 = afterMemberList[0];
        /// string new_team_of_member1 = member1[0];
        /// </code>
        /// </example>
        static public List<List<string>> ReformTeam(List<List<string>> beforeMemberList, int beforeTeamKeyIndex, int afterTeamNum)
        {
            return ReformTeam(beforeMemberList, beforeTeamKeyIndex, afterTeamNum, TRY_NUM);
        }

        /// <summary>
        /// Reform(convert) Team structure
        /// <see cref="ReformTeam(List{List{string}}, int, int)">
        /// Get "tryNum" number of reformed results, and return best scored(well-separated) result. 
        /// </summary>
        /// <param name="beforeMemberList">Target members list</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of beforeMemberList is before-Team's key</param>
        /// <param name="afterTeamNum">Number of after-Team to be devided</param>
        /// <param name="tryNum">Number of reform try times</param>
        /// <returns>Reformed members list</returns>
        static public List<List<string>> ReformTeam(List<List<string>> beforeMemberList, int beforeTeamKeyIndex, int afterTeamNum, int tryNum)
        {
            if (tryNum <= 0)
            {
                return new List<List<string>>();
            }

            // *** try a specific number of times ***
            var result = ReformTeam_Body(beforeMemberList, beforeTeamKeyIndex, afterTeamNum);
            for (int i = 0; i < tryNum - 1; i++)
            {
                var tryResult = ReformTeam_Body(beforeMemberList, beforeTeamKeyIndex, afterTeamNum);
                if (ReformScore(result, beforeTeamKeyIndex + 1) > ReformScore(tryResult, beforeTeamKeyIndex + 1))
                {
                    // select well-shuffled result
                    result = tryResult;
                }
            }
            return result;
        }

        /// <summary>
        /// Return team-reforming score
        /// </summary>
        /// <param name="afterMemberList">Reformed members list</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of afterMemberList is before-Team's key</param>
        /// <returns>team-reforming score (lower is better-shuffled)</returns>
        static public int ReformScore(List<List<string>> afterMemberList, int beforeTeamKeyIndex)
        {
            int result = 0;

            // *** create "after-Team" - "before-Team member" map ***
            var teamMap = afterMemberList.Aggregate(new Dictionary<string, List<List<string>>>(), (map, member) =>
            {
                if (!map.ContainsKey(member[0]))
                {
                    map[member[0]] = new List<List<string>>();
                }
                map[member[0]].Add(member);
                return map;
            });

            // *** create after team member map ***
            int numberOfNoShuffledTeam = 0;
            int numberOfSameTeammate = 0;
            int numberOfSameTeamCombo = 0;
            var beforeTeamkeylist = new List<List<string>>();
            foreach (string key in teamMap.Keys)
            {
                var beforeTeamList = teamMap[key].Select(mem => mem[beforeTeamKeyIndex]).ToList();
                beforeTeamList.Sort();
                beforeTeamkeylist.Add(beforeTeamList);

                int distinct = beforeTeamList.Distinct().Count();
                // How many no shufled team ?
                numberOfNoShuffledTeam += (distinct == 1) ? 1 : 0;

                // How many teammate of same before-Team ?
                numberOfSameTeammate += beforeTeamList.Count() - distinct;
            }
            // How many same before-Team combination ?
            numberOfSameTeamCombo += beforeTeamkeylist.Count() - DistinctList(beforeTeamkeylist).Count();

            result += numberOfNoShuffledTeam * 100;
            result += numberOfSameTeammate * 10;
            result += numberOfSameTeamCombo * 1;
            return result;
        }

        /// <summary>
        /// return list has unique list<string>
        /// </summary>
        static public List<List<string>> DistinctList(List<List<string>> list)
        {
            var targetList = ((List<string>[])list.ToArray().Clone()).ToList();
            var distinctList = list.Where(val =>
            {
                targetList.Remove(val);
                foreach (var orgList in targetList)
                {
                    bool isAllElementsSame = true;
                    for (int i = 0; i < orgList.Count; i++)
                    {
                        if (orgList[i] != val[i])
                        {
                            isAllElementsSame = false;
                            break;
                        }
                    }
                    if (isAllElementsSame)
                    {
                        // duplicated
                        return false;
                    }
                }
                return true;
            });

            return distinctList.ToList();
        }

        static public List<List<string>> ReformTeam_Body(List<List<string>> beforeMemberList, int beforeTeamKeyIndex, int afterTeamNum)
        {
            // *** init ***
            var result = new List<List<string>>();
            // convert flat list of before-Team members, to "before-Team name"-"list of members" map
            var beforeTeamMemberMap = ConvertTeamMemberMappping(beforeMemberList, beforeTeamKeyIndex);

            // *** Reform ***
            // convert to "after-Team name"-"list of members" map
            var memberAssignedAfterMap = AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, afterTeamNum, true);

            // *** change to list ***
            foreach (string afterTeamName in memberAssignedAfterMap.Keys)
            {
                foreach (List<string> memberInfo in memberAssignedAfterMap[afterTeamName])
                {
                    // convert member info to List<string>
                    // 1st element of list is after-Team's name(like "0", "1", "2", ..., "16")
                    // and 2nd, 3rd, 4th are member information
                    result.Add(new string[] { afterTeamName, memberInfo[0], memberInfo[1], memberInfo[2] }.ToList());
                }
            }
            return result;
        }

        /// <summary>
        /// Convert flat list of Team members, to "Team name"-"list of members" map
        /// </summary>
        /// <param name="memberList">Flat list of Team members</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of memberList is Team's key</param>
        /// <returns>"Team name"-"list of members" map</returns>
        static public Dictionary<string, List<List<string>>> ConvertTeamMemberMappping(
            List<List<string>> memberList, int beforeTeamKeyIndex)
        {
            var result = new Dictionary<string, List<List<string>>>();

            foreach (List<string> memberInfo in memberList)
            {
                string teamKey = memberInfo[beforeTeamKeyIndex];
                if (!result.ContainsKey(teamKey))
                {
                    result[teamKey] = new List<List<string>>();
                }
                result[teamKey].Add(memberInfo);
            }

            return result;
        }

        /// <summary>
        /// Assign before-Team members to after-Team
        /// </summary>
        /// <param name="beforeTeamMemberMap">"before-Team name"-"list of members" map</param>
        /// <param name="afterTeamNum">number of after-Teams</param>
        /// <param name="isShuffle">whether shuffle result or not</param>
        /// <returns>"after-Team name"-"list of members" map(Team's name is number(like "0", "1", "2", ..., "16"))</returns>
        static public Dictionary<string, List<List<string>>> AssignBeforeTeamMemberToAfterTeam(
            Dictionary<string, List<List<string>>> beforeTeamMemberMap, int afterTeamNum, bool isShuffle)
        {
            // init
            // create after-Team Name by "1", "2", "3", ...
            var result = new Dictionary<string, List<List<string>>>();
            for (int i = 0; i < afterTeamNum; i++)
            {
                result[i.ToString()] = new List<List<string>>();
            }
            // init for shuffle
            var beforeTeamChooseOrder = beforeTeamMemberMap.Keys.ToList();
            if (isShuffle)
            {
                // Order of assigned before-Team is shuffled
                var r = new Random();
                beforeTeamChooseOrder = beforeTeamChooseOrder.OrderBy(a => r.Next()).ToList();
            }
            var AfterTeamChooseOrder = result.Keys.ToList();
            int afterTeamIndex = AfterTeamChooseOrder.Count; // this will reset at following step

            // assign before-Team to after-Team
            // * At first, all member in same before-Team(="beforeTeamName") going to be assigned to each after-Teams.
            //   Assigned target after-Team will change cyclically (by "AfterTeamChooseOrder").
            //   So, a before-Team member may not be assigned to after-Team which has already have same before-Team member.
            foreach (string beforeTeamName in beforeTeamChooseOrder)
            {
                var teamMembers = beforeTeamMemberMap[beforeTeamName];
                if (isShuffle)
                {
                    // Order of assigning member will be shuffled
                    var r = new Random();
                    teamMembers = teamMembers.OrderBy(a => r.Next()).ToList();
                }
                foreach (List<string> member in teamMembers)
                {
                    if (afterTeamIndex >= AfterTeamChooseOrder.Count)
                    {
                        afterTeamIndex = 0;
                        if (isShuffle)
                        {
                            // Order of assigning after-Team will be shuffled at every loop
                            var r = new Random();
                            AfterTeamChooseOrder = AfterTeamChooseOrder.OrderBy(a => r.Next()).ToList();
                        }
                    }

                    string afterTeamName = AfterTeamChooseOrder[afterTeamIndex];
                    // assign "member" (which was in before-Team="beforeTeamName"), to after-Team(="afterTeamName")
                    result[afterTeamName].Add(member);

                    afterTeamIndex++;
                }
            }
            if (isShuffle)
            {
                var renewResult = new Dictionary<string, List<List<string>>>();
                foreach (string afterTeam in result.Keys)
                {
                    var r = new Random();
                    renewResult[afterTeam] = result[afterTeam].OrderBy(a => r.Next()).ToList();
                }
                result = renewResult;
            }

            return result;
        }
    }
}
