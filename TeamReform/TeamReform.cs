using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform
{
    static public class TeamReform
    {
        /// <summary>
        /// Reform(convert) Team structure
        /// </summary>
        /// <param name="beforeMemberList">Team-Member list(Outer List:each member, Inner List:info of the member)</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of beforeMemberList is before Team's key</param>
        /// <param name="afterTeamNum">Number of Team to be devided</param>
        /// <returns></returns>
        /// <example>
        /// Before Team:12 Teams, 4 people in each Team<br>
        /// After Team:16 Teams, 3 people in each Team<br>
        /// <code>
        /// // *** Prepare input data ***
        /// // All member in before Team must be added in flat 
        /// List<List<String>> beforeMemberList = new List<List<String>>();
        /// // member from Before Team1 (Team key is index 1)
        /// beforeMemberList.Add(new String[]{"MEMBER1fromT1", "TEAM1", "INFO1-1-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER2fromT1", "TEAM1", "INFO1-2-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER3fromT1", "TEAM1", "INFO1-3-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER4fromT1", "TEAM1", "INFO1-4-1", ...});
        /// // member from Before Team2
        /// ...
        /// // member from Before Team12 (Team key is index 1)
        /// beforeMemberList.Add(new String[]{"MEMBER1fromT12", "TEAM12", "INFO12-1-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER2fromT12", "TEAM12", "INFO12-2-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER3fromT12", "TEAM12", "INFO12-3-1", ...});
        /// beforeMemberList.Add(new String[]{"MEMBER4fromT12", "TEAM12", "INFO12-4-1", ...});
        /// 
        /// int afterTeamNum = 16;
        /// 
        /// // *** Call method ***
        /// // Reform "12 Team x 4 People" structure to "16 Team x 3 People" 
        /// List<List<String>> afterMemberList = ReformTeam(beforeMemberList, 1, afterTeamNum);
        /// 
        /// // *** Use result ***
        /// // afterMemberList consists of all member in after Team in flat
        /// // Each after Team's name is number(like "0", "1", "2", ..., "16")
        /// List<String> member1 = afterMemberList[0];
        /// int new_team_of_member1 = member1[0];
        /// </code>
        /// </example>
        static public List<List<String>> ReformTeam(List<List<String>> beforeMemberList, int beforeTeamKeyIndex, int afterTeamNum)
        {
            var result = new List<List<String>>();
            // convert flat list of before Team members, to "before Team name"-"list of members" map
            var beforeTeamMemberMap = ConvertTeamMemberMappping(beforeMemberList, beforeTeamKeyIndex, true);
            // convert "before Team name"-"list of members" map to "after Team name"-"list of members" map
            var memberAsignedAfterMap = AsignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, afterTeamNum, true);

            foreach (String afterTeamName in memberAsignedAfterMap.Keys)
            {
                foreach (List<String> memberInfo in memberAsignedAfterMap[afterTeamName])
                {
                    // convert member info to List<String>
                    // 1st element of list is after Team's name(like "0", "1", "2", ..., "16")
                    // and 2nd, 3rd, 4th are member information
                    result.Add(new String[] { afterTeamName, memberInfo[0], memberInfo[1], memberInfo[2] }.ToList());
                }
            }

            return result;
        }

        /// <summary>
        /// Convert flat list of Team members, to "Team name"-"list of members" map
        /// </summary>
        /// <param name="memberList">Flat list of Team members</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of memberList is Team's key</param>
        /// <param name="isShuffle">whether shuffle result or not</param>
        /// <returns>"Team name"-"list of members" map</returns>
        static public Dictionary<String, List<List<String>>> ConvertTeamMemberMappping(List<List<String>> memberList, int beforeTeamKeyIndex, bool isShuffle)
        {
            var result = new Dictionary<String, List<List<String>>>();

            foreach (List<String> memberInfo in memberList)
            {
                String teamKey = memberInfo[beforeTeamKeyIndex];
                if (!result.ContainsKey(teamKey))
                {
                    result[teamKey] = new List<List<String>>();
                }
                result[teamKey].Add(memberInfo);
            }

            if (isShuffle)
            {
                var newresult = new Dictionary<String, List<List<String>>>();
                foreach (String team in result.Keys)
                {
                    // shuffle member order in each Team
                    newresult[team] = result[team].OrderBy(a => Guid.NewGuid()).ToList();
                }
                result = newresult;
            }

            return result;
        }

        /// <summary>
        /// Asign before Team members to after Team
        /// </summary>
        /// <param name="beforeTeamMemberMap">"before Team name"-"list of members" map</param>
        /// <param name="afterTeamNum">number of after Teams</param>
        /// <param name="isShuffle">whether shuffle result or not</param>
        /// <returns>"after Team name"-"list of members" map(Team's name is number(like "0", "1", "2", ..., "16"))</returns>
        static public Dictionary<String, List<List<String>>> AsignBeforeTeamMemberToAfterTeam(Dictionary<String, List<List<String>>> beforeTeamMemberMap, int afterTeamNum, bool isShuffle)
        {
            // init
            // create after Team Name by "1", "2", "3", ...
            var result = new Dictionary<String, List<List<String>>>();
            for (int i = 0; i < afterTeamNum; i++)
            {
                result[i.ToString()] = new List<List<String>>();
            }

            // init for shuffle
            var beforeTeamChooseOrder = beforeTeamMemberMap.Keys.ToList();
            if (isShuffle)
            {
                // Order of asigned before Team shuffled
                beforeTeamChooseOrder = beforeTeamChooseOrder.OrderBy(a => Guid.NewGuid()).ToList();
            }
            var AfterTeamChooseOrder = result.Keys.ToList();
            int afterTeamIndex = AfterTeamChooseOrder.Count; // this will reset at following step

            // asign before Team to after Team
            foreach (String beforeTeamName in beforeTeamChooseOrder)
            {
                foreach (List<String> member in beforeTeamMemberMap[beforeTeamName])
                {
                    if (afterTeamIndex >= AfterTeamChooseOrder.Count)
                    {
                        afterTeamIndex = 0;
                        if (isShuffle)
                        {
                            // Order of asigning after Team will be shuffled at every loop
                            AfterTeamChooseOrder = AfterTeamChooseOrder.OrderBy(a => Guid.NewGuid()).ToList();
                        }
                    }

                    String afterTeamName = AfterTeamChooseOrder[afterTeamIndex];
                    // set before Team member, to after Team
                    result[afterTeamName].Add(member);

                    afterTeamIndex++;
                }
            }

            return result;
        }
    }
}
