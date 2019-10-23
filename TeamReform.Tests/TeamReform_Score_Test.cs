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
        private float createScoreValue(int i1, int i2, int[] i3List)
        {
            if (i3List == null)
            {
                i3List = new int[3];
            }
            return (i1 * 100f) + (i2 * 10f)
                + (i3List[0] * 1f + i3List[1] * 1f / 4.0f + i3List[2] * 1f / 16.0f);
        }

        [Fact]
        public void TeamScore_3x4_3x4_wellsuffled()
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T1, T1, T2, T3
            // after 2 = T1, T2, T2, T3
            // after 3 = T1, T2, T3, T3
            // no shuffle 0
            // same teammate 3
            // same team combination [3]
            Assert.Equal(createScoreValue(0, 3, new int[] { 3, 0, 0, 0 }), score);
        }

        [Fact]
        public void TeamScore_3x4_3x4_littlesuffled()
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T2, T2, T3, T3
            // after 2 = T1, T1, T1, T1
            // after 3 = T2, T2, T3, T3
            // no shuffle 1
            // same teammate 2+3+2
            // same team combination [1, 0, 0, 0]
            Assert.Equal(createScoreValue(1, 7, new int[] { 1, 0, 0, 0 }), score);
        }

        [Fact]
        public void TeamScore_3x4_3x4_nosuffled()
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T2, T2, T2, T2
            // after 2 = T1, T1, T1, T1
            // after 3 = T3, T3, T3, T3
            // no shuffle 3
            // same teammate 3+3+3
            // same team combination [0]
            Assert.Equal(createScoreValue(3, 9, null), score);
        }

        [Fact]
        public void TeamScore_3x4_4x3_samecombi()
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T1, T2, T3
            // after 2 = T1, T2, T3
            // after 3 = T1, T2, T3
            // after 4 = T1, T2, T3
            // no shuffle 0
            // same teammate 0
            // same team combination [1+2+3, 0, 0, 0]
            Assert.Equal(createScoreValue(0, 0, new int[] { 6, 0, 0, 0 }), score);
        }

        [Fact]
        public void TeamScore_3x4_4x3_littlesuffled()
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T2, T3, T3
            // after 2 = T1, T1, T1
            // after 3 = T2, T2, T3
            // after 4 = T1, T2, T3
            // no shuffle 1
            // same teammate 1+2+1
            // same team combination [1, 2]
            Assert.Equal(createScoreValue(1, 4, new int[] { 1, 2, 0, 0 }), score);
        }

        [Fact]
        public void TeamScore_3x4incomplete_4xx()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "2", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "4", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "4", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT3", "TEAM3", "INFO3-4-1" }.ToList(),
            };

            // *** Call method ***
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T2
            // after 2 = T1, T1
            // after 3 = T2, T2, T3
            // after 4 = T1, T2, T3
            // no shuffle 2
            // same teammate 2
            // same team combination [0]
            Assert.Equal(createScoreValue(2, 2, new int[] { 0, 1, 0, 0 }), score);
        }

        [Fact]
        public void TeamScore_3x4incomplete_4x2()
        {
            // *** Prepare input data ***
            // All member must be added in flat 
            var afterMemberList = new List<List<String>>()
            {
                // member from Team1
                new String[] { "1", "MEMBER1fromT1", "TEAM1", "INFO1-1-1" }.ToList(),
                new String[] { "2", "MEMBER2fromT1", "TEAM1", "INFO1-2-1" }.ToList(),
                new String[] { "4", "MEMBER4fromT1", "TEAM1", "INFO1-4-1" }.ToList(),
                // member from Team2
                new String[] { "2", "MEMBER1fromT2", "TEAM2", "INFO2-1-1" }.ToList(),
                new String[] { "1", "MEMBER2fromT2", "TEAM2", "INFO2-2-1" }.ToList(),
                new String[] { "3", "MEMBER3fromT2", "TEAM2", "INFO2-3-1" }.ToList(),
                new String[] { "3", "MEMBER4fromT2", "TEAM2", "INFO2-4-1" }.ToList(),
                // member from Team3
                new String[] { "4", "MEMBER3fromT3", "TEAM3", "INFO3-3-1" }.ToList(),
            };

            // *** Call method ***
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T1, T2
            // after 2 = T1, T2
            // after 3 = T2, T2
            // after 4 = T1, T3
            // no shuffle 1
            // same teammate 1
            // same team combination [1]
            Assert.Equal(createScoreValue(1, 1, new int[] { 1, 0, 0, 0 }), score);
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
            float score = TeamReform.ReformScore(afterMemberList, 0, 2);

            // after 1 = T1, T2, T3, T8
            // after 2 = T3, T4, T8, T12
            // after 3 = T1, T4, T5, T8
            // after 4 = T1, T5, T6, T9
            // after 5 = T2, T3, T4, T9
            // after 6 = T2, T4, T5, T10
            // after 7 = T2, T5, T6, T11
            // after 8 = T3, T6, T7, T11
            // after 9 = T6, T8, T9, T12
            // after10 = T7, T9, T10, T12
            // after11 = T7, T10, T11, T12
            // after12 = T1, T7, T10, T11
            // no shuffle 0
            // same teammate 0
            // same team combination [0, 2, 1+2+1+2+2+2+1+2+1+1+2, 0]
            Assert.Equal(createScoreValue(0, 0, new int[] { 0, 2, 17, 0 }), score);
        }
    }
}