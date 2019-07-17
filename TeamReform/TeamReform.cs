﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform
{
    static public class TeamReform
    {
        /// <summary>
        /// Reform(convert) Team structure
        /// </summary>
        /// <param name="beforeMemberList">Target members list</param>
        /// <param name="beforeTeamKeyIndex">Designate which index of beforeMemberList is before-Team's key</param>
        /// <param name="afterTeamNum">Number of after-Team to be devided</param>
        /// <returns></returns>
        /// <example>
        /// Before:12 Teams, 4 people in each Team<br>
        /// After :16 Teams, 3 people in each Team<br>
        /// <code>
        /// // *** Prepare input data ***
        /// // All member in before-Team must be added in flat 
        /// List<List<String>> beforeMemberList = new List<List<String>>()
        /// {
        ///     // member from before-Team1 (Team key is index 1)
        ///     new String[] { "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
        ///     new String[] { "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
        ///     new String[] { "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
        ///     new String[] { "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
        ///     // member from before-Team2 (Team key is index 1)
        ///     new String[] { "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
        ///     ...
        ///     // member from before-Team12 (Team key is index 1)
        ///     new String[] { "MEMBER1fromT12", "TEAM12", "INFO12-1-1" }.ToList(),
        ///     new String[] { "MEMBER2fromT12", "TEAM12", "INFO12-2-1" }.ToList(),
        ///     new String[] { "MEMBER3fromT12", "TEAM12", "INFO12-3-1" }.ToList(),
        ///     new String[] { "MEMBER4fromT12", "TEAM12", "INFO12-4-1" }.ToList(),
        /// };
        /// 
        /// int afterTeamNum = 16;
        /// 
        /// // *** Call method ***
        /// // Reform "12 Team x 4 People" structure to "16 Team x 3 People" 
        /// List<List<String>> afterMemberList = ReformTeam(beforeMemberList, 1, afterTeamNum);
        /// 
        /// // *** Use result ***
        /// // afterMemberList consists of all member which has after-Team's name, in flat
        /// // Each after-Team's name is number string(like "0", "1", "2", ..., "16")
        /// List<String> member1 = afterMemberList[0];
        /// String new_team_of_member1 = member1[0];
        /// </code>
        /// </example>
        static public List<List<String>> ReformTeam(List<List<String>> beforeMemberList, int beforeTeamKeyIndex, int afterTeamNum)
        {
            // *** init ***
            var result = new List<List<String>>();
            // convert flat list of before-Team members, to "before-Team name"-"list of members" map
            var beforeTeamMemberMap = ConvertTeamMemberMappping(beforeMemberList, beforeTeamKeyIndex);

            // *** Reform ***
            // convert to "after-Team name"-"list of members" map
            var memberAssignedAfterMap = AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, afterTeamNum, true);

            // *** change to list ***
            foreach (String afterTeamName in memberAssignedAfterMap.Keys)
            {
                foreach (List<String> memberInfo in memberAssignedAfterMap[afterTeamName])
                {
                    // convert member info to List<String>
                    // 1st element of list is after-Team's name(like "0", "1", "2", ..., "16")
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
        /// <returns>"Team name"-"list of members" map</returns>
        static public Dictionary<String, List<List<String>>> ConvertTeamMemberMappping(
            List<List<String>> memberList, int beforeTeamKeyIndex)
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

            return result;
        }

        /// <summary>
        /// Assign before-Team members to after-Team
        /// </summary>
        /// <param name="beforeTeamMemberMap">"before-Team name"-"list of members" map</param>
        /// <param name="afterTeamNum">number of after-Teams</param>
        /// <param name="isShuffle">whether shuffle result or not</param>
        /// <returns>"after-Team name"-"list of members" map(Team's name is number(like "0", "1", "2", ..., "16"))</returns>
        static public Dictionary<String, List<List<String>>> AssignBeforeTeamMemberToAfterTeam(
            Dictionary<String, List<List<String>>> beforeTeamMemberMap, int afterTeamNum, bool isShuffle)
        {
            // init
            // create after-Team Name by "1", "2", "3", ...
            var result = new Dictionary<String, List<List<String>>>();
            for (int i = 0; i < afterTeamNum; i++)
            {
                result[i.ToString()] = new List<List<String>>();
            }
            // init for shuffle
            var beforeTeamChooseOrder = beforeTeamMemberMap.Keys.ToList();
            if (isShuffle)
            {
                // Order of assigned before-Team is shuffled
                beforeTeamChooseOrder = beforeTeamChooseOrder.OrderBy(a => Guid.NewGuid()).ToList();
            }
            var AfterTeamChooseOrder = result.Keys.ToList();
            int afterTeamIndex = AfterTeamChooseOrder.Count; // this will reset at following step

            // assign before-Team to after-Team
            // * At first, all member in same before-Team(="beforeTeamName") going to be assigned to each after-Teams.
            //   Assigned target after-Team will change cyclically (by "AfterTeamChooseOrder").
            //   So, a before-Team member may not be assigned to after-Team which has already have same before-Team member.
            foreach (String beforeTeamName in beforeTeamChooseOrder)
            {
                var teamMembers = beforeTeamMemberMap[beforeTeamName];
                if (isShuffle)
                {
                    // Order of assigning member will be shuffled
                    teamMembers = teamMembers.OrderBy(a => Guid.NewGuid()).ToList();
                }
                foreach (List<String> member in teamMembers)
                {
                    if (afterTeamIndex >= AfterTeamChooseOrder.Count)
                    {
                        afterTeamIndex = 0;
                        if (isShuffle)
                        {
                            // Order of assigning after-Team will be shuffled at every loop
                            AfterTeamChooseOrder = AfterTeamChooseOrder.OrderBy(a => Guid.NewGuid()).ToList();
                        }
                    }

                    String afterTeamName = AfterTeamChooseOrder[afterTeamIndex];
                    // assign "member" (which was in before-Team="beforeTeamName"), to after-Team(="afterTeamName")
                    result[afterTeamName].Add(member);

                    afterTeamIndex++;
                }
            }

            return result;
        }
    }
}
