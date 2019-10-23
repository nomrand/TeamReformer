using Xunit;
using Xunit.Abstractions;
using TeamReform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamReform.UnitTests
{
    public class TeamReform_SCore_Test
    {
        [Fact]
        public void TeamScore_4x3_4x3_wellsuffled()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "1", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "1", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "2", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "3", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "1", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "2", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "3", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "2", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T1, T1, T2, T3
            // after 2 = T1, T2, T2, T3
            // after 3 = T1, T2, T3, T3
            Assert.Equal(10 * 3 + 1 * 0, score);
        }

        [Fact]
        public void TeamScore_4x3_4x3_littlesuffled()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "2", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "2", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "2", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "1", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "1", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // no shuffle * 1
            // same teammate 3+2+2
            // same team combination 1
            Assert.Equal(100 * 1 + 10 * 7 + 1 * 1, score);
        }

        [Fact]
        public void TeamScore_4x3_4x3_nosuffled()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "2", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "2", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "2", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "1", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "1", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "1", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "3", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "3", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // no shuffle * 3
            // same teammate 3*3
            // same team combination 0
            Assert.Equal(100 * 3 + 10 * 9 + 1 * 0, score);
        }

        [Fact]
        public void TeamScore_4x3_3x4_samecombi()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "1", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "1", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "1", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // no shuffle 0
            // same teammate 0
            // same team combination 3
            Assert.Equal(100 * 0 + 10 * 0 + 1 * 3, score);
        }

        [Fact]
        public void TeamScore_4x3_3x4_littlesuffled()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "2", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "2", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "4", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "1", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "4", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // no shuffle 1
            // same teammate 2+1+1
            // same team combination 0
            Assert.Equal(100 * 1 + 10 * 4 + 1 * 0, score);
        }

        [Fact]
        public void TeamScore_12x4_12x4()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "1", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "12", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT1", "TEAM1", "INFO1-3-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "1", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "5", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "6", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "7", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "1", "MEMBER1fromT3", "TEAM3", "INFO3-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT3", "TEAM3", "INFO3-2-1" }.ToList(),
                new String[] { "5", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "8", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
                // member from Team4
                new String[] { "2", "MEMBER1fromT4", "TEAM4", "INFO4-1-1" }.ToList(),
                new String[] { "3", "MEMBER2fromT4", "TEAM4", "INFO4-2-1" }.ToList(),
                new String[] { "5", "MEMBER3fromT4", "TEAM4", "INFO4-3-1" }.ToList(),
                new String[] { "6", "MEMBER4fromT4", "TEAM4", "INFO4-4-1" }.ToList(),
                // member from Team5
                new String[] { "3", "MEMBER1fromT5", "TEAM5", "INFO5-1-1" }.ToList(),
                new String[] { "4", "MEMBER2fromT5", "TEAM5", "INFO5-2-1" }.ToList(),
                new String[] { "6", "MEMBER3fromT5", "TEAM5", "INFO5-3-1" }.ToList(),
                new String[] { "7", "MEMBER4fromT5", "TEAM5", "INFO5-4-1" }.ToList(),
                // member from Team6
                new String[] { "4", "MEMBER1fromT6", "TEAM6", "INFO6-1-1" }.ToList(),
                new String[] { "7", "MEMBER2fromT6", "TEAM6", "INFO6-2-1" }.ToList(),
                new String[] { "8", "MEMBER3fromT6", "TEAM6", "INFO6-3-1" }.ToList(),
                new String[] { "9", "MEMBER4fromT6", "TEAM6", "INFO6-4-1" }.ToList(),
                // member from Team7
                new String[] { "8", "MEMBER1fromT7", "TEAM7", "INFO7-1-1" }.ToList(),
                new String[] { "10", "MEMBER2fromT7", "TEAM7", "INFO7-2-1" }.ToList(),
                new String[] { "11", "MEMBER3fromT7", "TEAM7", "INFO7-3-1" }.ToList(),
                new String[] { "12", "MEMBER4fromT7", "TEAM7", "INFO7-4-1" }.ToList(),
                // member from Team8
                new String[] { "9", "MEMBER1fromT8", "TEAM8", "INFO8-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT8", "TEAM8", "INFO8-2-1" }.ToList(),
                new String[] { "2", "MEMBER3fromT8", "TEAM8", "INFO8-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT8", "TEAM8", "INFO8-4-1" }.ToList(),
                // member from Team9
                new String[] { "9", "MEMBER1fromT9", "TEAM9", "INFO9-1-1" }.ToList(),
                new String[] { "10", "MEMBER2fromT9", "TEAM9", "INFO9-2-1" }.ToList(),
                new String[] { "4", "MEMBER3fromT9", "TEAM9", "INFO9-3-1" }.ToList(),
                new String[] { "5", "MEMBER4fromT9", "TEAM9", "INFO9-4-1" }.ToList(),
                // member from Team10
                new String[] { "10", "MEMBER1fromT10", "TEAM10", "INFO10-1-1" }.ToList(),
                new String[] { "11", "MEMBER2fromT10", "TEAM10", "INFO10-2-1" }.ToList(),
                new String[] { "12", "MEMBER3fromT10", "TEAM10", "INFO10-3-1" }.ToList(),
                new String[] { "6", "MEMBER4fromT10", "TEAM10", "INFO10-4-1" }.ToList(),
                // member from Team11
                new String[] { "11", "MEMBER1fromT11", "TEAM11", "INFO11-1-1" }.ToList(),
                new String[] { "12", "MEMBER2fromT11", "TEAM11", "INFO11-2-1" }.ToList(),
                new String[] { "7", "MEMBER3fromT11", "TEAM11", "INFO11-3-1" }.ToList(),
                new String[] { "8", "MEMBER4fromT11", "TEAM11", "INFO11-4-1" }.ToList(),
                // member from Team12
                new String[] { "2", "MEMBER1fromT12", "TEAM12", "INFO12-1-1" }.ToList(),
                new String[] { "9", "MEMBER2fromT12", "TEAM12", "INFO12-2-1" }.ToList(),
                new String[] { "10", "MEMBER3fromT12", "TEAM12", "INFO12-3-1" }.ToList(),
                new String[] { "11", "MEMBER4fromT12", "TEAM12", "INFO12-4-1" }.ToList(),
            };

            // *** Call method ***
            int score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // no shuffle 0
            // same teammate 0
            // same team combination 0
            Assert.Equal(100 * 0 + 10 * 0 + 1 * 0, score);
        }
    }
}