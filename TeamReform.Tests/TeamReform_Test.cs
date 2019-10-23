using Xunit;
using TeamReform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform.UnitTests
{
    public class TeamReform_Test
    {
        [Fact]
        public void ReformTeam_3x4_to_4x3()
        {
            // *** Prepare input data ***
            // All member in before Team must be added in flat 
            var beforeMemberList = new List<List<String>>()
            {
                // member from Before Team1 (Team key is index 1)
                new String[] { "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Before Team2 (Team key is index 1)
                new String[] { "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Before Team12 (Team key is index 1)
                new String[] { "MEMBER1fromT12", "TEAM12", "INFO12-1-1" }.ToList(),
                new String[] { "MEMBER2fromT12", "TEAM12", "INFO12-2-1" }.ToList(),
                new String[] { "MEMBER3fromT12", "TEAM12", "INFO12-3-1" }.ToList(),
                new String[] { "MEMBER4fromT12", "TEAM12", "INFO12-4-1" }.ToList(),
            };

            int afterTeamNum = 4;

            // *** Call method ***
            List<List<String>> afterMemberList = TeamReform.ReformTeam_Body(beforeMemberList.ToList(), 1, afterTeamNum);

            // *** Use result ***
            // check all before member exists
            Assert.Equal(beforeMemberList.Count, afterMemberList.Count);
            foreach (List<String> beforeMember in beforeMemberList)
            {
                bool isExist = false;
                foreach (List<String> afterMember in afterMemberList)
                {
                    bool match =
                    (beforeMember[0] == afterMember[1]) &&
                    (beforeMember[1] == afterMember[2]) &&
                    (beforeMember[2] == afterMember[3]);

                    if (match)
                    {
                        isExist = true;
                        break;
                    }
                }
                Assert.True(isExist);
            }

            var teamMemberMap = new Dictionary<String, List<List<String>>>();
            teamMemberMap["0"] = new List<List<String>>();
            teamMemberMap["1"] = new List<List<String>>();
            teamMemberMap["2"] = new List<List<String>>();
            teamMemberMap["3"] = new List<List<String>>();
            // check after team has same number of members
            foreach (List<String> afterMember in afterMemberList)
            {
                teamMemberMap[afterMember[0]].Add(afterMember);
            }
            Assert.Equal(3, teamMemberMap["0"].Count);
            Assert.Equal(3, teamMemberMap["1"].Count);
            Assert.Equal(3, teamMemberMap["2"].Count);
            Assert.Equal(3, teamMemberMap["3"].Count);

            // check in a after Team has only 1 of same before Team member
            foreach (List<List<String>> memberList in teamMemberMap.Values)
            {
                Assert.Equal("TEAM", memberList[0][2].Substring(0, 4));
                Assert.Equal("TEAM", memberList[1][2].Substring(0, 4));
                Assert.Equal("TEAM", memberList[2][2].Substring(0, 4));
                Assert.False(memberList[0][2] == memberList[1][2]);
                Assert.False(memberList[1][2] == memberList[2][2]);
                Assert.False(memberList[2][2] == memberList[0][2]);
            }
        }

        [Fact]
        public void AsignBeforeTeamMemberToAfterTeam_3x4_to_4x3()
        {
            // Before:3Teams, each Team has 4 people
            var beforeTeamMemberMap = new Dictionary<String, List<List<String>>>()
            {
                {"TEAM1", new List<List<String>>() {
                    new List<String>() { "1", "NAME1_1" },
                    new List<String>() { "2", "NAME1_2" },
                    new List<String>() { "3", "NAME1_2" },
                    new List<String>() { "4", "NAME1_2" },
                }},
                {"TEAM2", new List<List<String>>() {
                    new List<String>() { "5", "NAME2_1" },
                    new List<String>() { "6", "NAME2_2" },
                    new List<String>() { "7", "NAME2_2" },
                    new List<String>() { "8", "NAME2_2" },
                }},
                {"TEAM3", new List<List<String>>() {
                    new List<String>() { "9", "NAME3_1" },
                    new List<String>() { "10", "NAME3_2" },
                    new List<String>() { "11", "NAME3_3" },
                    new List<String>() { "12", "NAME3_4" },
                }},
            };

            // After :4Teams, each Team has 3 people
            var result = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 4, false);

            // List size check
            Assert.Equal(4, result.Keys.Count);
            foreach (String team in result.Keys)
            {
                Assert.Equal(3, result[team].Count);
            }

            // Contents check
            Assert.Equal(beforeTeamMemberMap["TEAM1"][0], result["0"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][1], result["1"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][2], result["2"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][3], result["3"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][0], result["0"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][1], result["1"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][2], result["2"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][3], result["3"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][0], result["0"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][1], result["1"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][2], result["2"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][3], result["3"][2]);
        }

        [Fact]
        public void AsignBeforeTeamMemberToAfterTeam_3x4_to_2x6()
        {
            // Before:3Teams, each Team has 4 people
            var beforeTeamMemberMap = new Dictionary<String, List<List<String>>>()
            {
                {"TEAM1", new List<List<String>>() {
                    new List<String>() { "1", "NAME1_1" },
                    new List<String>() { "2", "NAME1_2" },
                    new List<String>() { "3", "NAME1_2" },
                    new List<String>() { "4", "NAME1_2" },
                }},
                {"TEAM2", new List<List<String>>() {
                    new List<String>() { "5", "NAME2_1" },
                    new List<String>() { "6", "NAME2_2" },
                    new List<String>() { "7", "NAME2_2" },
                    new List<String>() { "8", "NAME2_2" },
                }},
                {"TEAM3", new List<List<String>>() {
                    new List<String>() { "9", "NAME3_1" },
                    new List<String>() { "10", "NAME3_2" },
                    new List<String>() { "11", "NAME3_3" },
                    new List<String>() { "12", "NAME3_4" },
                }},
            };

            // After :2Teams, each Team has 6 people
            var result = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 2, false);

            // List size check
            Assert.Equal(2, result.Keys.Count);
            foreach (String team in result.Keys)
            {
                Assert.Equal(6, result[team].Count);
            }

            // Contents check
            // if not shuffled, Team will choose TEAM1->TEAM2->TEAM3
            Assert.Equal(beforeTeamMemberMap["TEAM1"][0], result["0"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][1], result["1"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][2], result["0"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][3], result["1"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][0], result["0"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][1], result["1"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][2], result["0"][3]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][3], result["1"][3]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][0], result["0"][4]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][1], result["1"][4]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][2], result["0"][5]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][3], result["1"][5]);
        }

        [Fact]
        public void AsignBeforeTeamMemberToAfterTeam_3x4_to_4x3_random___sometimes_failed()
        {
            // Before:3Teams, each Team has 4 people
            var beforeTeamMemberMap = new Dictionary<String, List<List<String>>>()
            {
                {"TEAM1", new List<List<String>>() {
                    new List<String>() { "1", "NAME1_1" },
                    new List<String>() { "2", "NAME1_2" },
                    new List<String>() { "3", "NAME1_2" },
                    new List<String>() { "4", "NAME1_2" },
                }},
                {"TEAM2", new List<List<String>>() {
                    new List<String>() { "5", "NAME2_1" },
                    new List<String>() { "6", "NAME2_2" },
                    new List<String>() { "7", "NAME2_2" },
                    new List<String>() { "8", "NAME2_2" },
                }},
                {"TEAM3", new List<List<String>>() {
                    new List<String>() { "9", "NAME3_1" },
                    new List<String>() { "10", "NAME3_2" },
                    new List<String>() { "11", "NAME3_3" },
                    new List<String>() { "12", "NAME3_4" },
                }},
            };

            // After :4Teams, each Team has 3 people
            var result1 = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 4, true);
            var result2 = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 4, true);

            // List size check
            Assert.Equal(4, result1.Keys.Count);
            Assert.Equal(4, result2.Keys.Count);
            // result1 & result2 should be defferent
            bool isDefferent = false;
            foreach (String team in result1.Keys)
            {
                Assert.Equal(3, result1[team].Count);
                Assert.Equal(3, result2[team].Count);

                isDefferent |= (result1[team][0] != result2[team][0]);
                isDefferent |= (result1[team][1] != result2[team][1]);
                isDefferent |= (result1[team][2] != result2[team][2]);
            }
            Assert.True(isDefferent, "result1 & result2 should be defferent, but sometimes this test is failed...");
        }

        [Fact]
        public void AsignBeforeTeamMemberToAfterTeam_3x4incomplete_to_4xx()
        {
            // Before:3Teams, each Team has 4 people
            var beforeTeamMemberMap = new Dictionary<String, List<List<String>>>()
            {
                {"TEAM1", new List<List<String>>() {
                    new List<String>() { "1", "NAME1_1" },
                    new List<String>() { "2", "NAME1_2" },
                    new List<String>() { "3", "NAME1_2" },
                }},
                {"TEAM2", new List<List<String>>() {
                    new List<String>() { "5", "NAME2_1" },
                }},
                {"TEAM3", new List<List<String>>() {
                    new List<String>() { "9", "NAME3_1" },
                    new List<String>() { "10", "NAME3_2" },
                    new List<String>() { "11", "NAME3_3" },
                }},
            };

            // After :4Teams, each Team has 3 people
            var result = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 4, false);

            // Contents check
            Assert.Equal(beforeTeamMemberMap["TEAM1"][0], result["0"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][1], result["1"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM1"][2], result["2"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][0], result["3"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][0], result["0"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][1], result["1"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][2], result["2"][1]);
        }

        [Fact]
        public void AsignBeforeTeamMemberToAfterTeam_3x4incomplete_to_2xx()
        {
            // Before:3Teams, each Team has 4 people
            var beforeTeamMemberMap = new Dictionary<String, List<List<String>>>()
            {
                {"TEAM1", new List<List<String>>() {
                    new List<String>() { "1", "NAME1_1" },
                }},
                {"TEAM2", new List<List<String>>() {
                    new List<String>() { "5", "NAME2_1" },
                    new List<String>() { "6", "NAME2_2" },
                    new List<String>() { "7", "NAME2_2" },
                    new List<String>() { "8", "NAME2_2" },
                }},
                {"TEAM3", new List<List<String>>() {
                    new List<String>() { "9", "NAME3_1" },
                    new List<String>() { "10", "NAME3_2" },
                }},
            };

            // After :2Teams, each Team has 6 people
            var result = TeamReform.AssignBeforeTeamMemberToAfterTeam(beforeTeamMemberMap, 2, false);

            // Contents check
            // if not shuffled, Team will choose TEAM1->TEAM2->TEAM3
            Assert.Equal(beforeTeamMemberMap["TEAM1"][0], result["0"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][0], result["1"][0]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][1], result["0"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][2], result["1"][1]);
            Assert.Equal(beforeTeamMemberMap["TEAM2"][3], result["0"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][0], result["1"][2]);
            Assert.Equal(beforeTeamMemberMap["TEAM3"][1], result["0"][3]);
        }


        [Fact]
        public void ConvertTeamMemberMappping_3x4()
        {
            // 3Teams, each Team has 4 people
            var flatMember = new List<List<String>>()
            {
                new List<String>(){"1", "TEAM1", "NAME1_1"},
                new List<String>(){"2", "TEAM2", "NAME2_1"},
                new List<String>(){"3", "TEAM3", "NAME3_1"},
                new List<String>(){"4", "TEAM1", "NAME1_2"},
                new List<String>(){"5", "TEAM2", "NAME2_2"},
                new List<String>(){"6", "TEAM3", "NAME3_2"},
                new List<String>(){"7", "TEAM1", "NAME1_3"},
                new List<String>(){"8", "TEAM2", "NAME2_3"},
                new List<String>(){"9", "TEAM3", "NAME3_3"},
                new List<String>(){"10", "TEAM1", "NAME1_4"},
                new List<String>(){"11", "TEAM2", "NAME2_4"},
                new List<String>(){"12", "TEAM3", "NAME3_4"},
            };

            var result = TeamReform.ConvertTeamMemberMappping(flatMember, 1);

            Assert.Equal(3, result.Keys.Count);
            Assert.Equal(4, result["TEAM1"].Count);
            Assert.Equal(flatMember[0], result["TEAM1"][0]);
            Assert.Equal(flatMember[3], result["TEAM1"][1]);
            Assert.Equal(flatMember[6], result["TEAM1"][2]);
            Assert.Equal(flatMember[9], result["TEAM1"][3]);

            Assert.Equal(4, result["TEAM2"].Count);
            Assert.Equal(flatMember[1], result["TEAM2"][0]);
            Assert.Equal(flatMember[4], result["TEAM2"][1]);
            Assert.Equal(flatMember[7], result["TEAM2"][2]);
            Assert.Equal(flatMember[10], result["TEAM2"][3]);

            Assert.Equal(4, result["TEAM3"].Count);
            Assert.Equal(flatMember[2], result["TEAM3"][0]);
            Assert.Equal(flatMember[5], result["TEAM3"][1]);
            Assert.Equal(flatMember[8], result["TEAM3"][2]);
            Assert.Equal(flatMember[11], result["TEAM3"][3]);
        }

        [Fact]
        public void ConvertTeamMemberMappping_3incompletex4()
        {
            // 3Teams, each Team has 4 people
            var flatMember = new List<List<String>>()
            {
                new List<String>(){"1", "TEAM1", "NAME1_1"},
                new List<String>(){"2", "TEAM2", "NAME2_1"},
                new List<String>(){"3", "TEAM3", "NAME3_1"},
                new List<String>(){"6", "TEAM3", "NAME3_2"},
                new List<String>(){"8", "TEAM2", "NAME2_3"},
                new List<String>(){"12", "TEAM3", "NAME3_4"},
            };

            var result = TeamReform.ConvertTeamMemberMappping(flatMember, 1);

            Assert.Equal(3, result.Keys.Count);
            Assert.Equal(1, result["TEAM1"].Count);
            Assert.Equal(flatMember[0], result["TEAM1"][0]);

            Assert.Equal(2, result["TEAM2"].Count);
            Assert.Equal(flatMember[1], result["TEAM2"][0]);
            Assert.Equal(flatMember[4], result["TEAM2"][1]);

            Assert.Equal(3, result["TEAM3"].Count);
            Assert.Equal(flatMember[2], result["TEAM3"][0]);
            Assert.Equal(flatMember[3], result["TEAM3"][1]);
            Assert.Equal(flatMember[5], result["TEAM3"][2]);
        }
    }
}