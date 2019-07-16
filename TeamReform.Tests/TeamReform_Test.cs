using Xunit;
using TeamReform;
using System;
using System.Collections.Generic;

namespace TeamReform.UnitTests
{
    public class TeamReform_Test
    {
        private readonly TeamReform _teamReform;

        public TeamReform_Test()
        {
            _teamReform = new TeamReform();
        }

        [Fact]
        public void SimpleTroupDevide_3x4_to_4x3()
        {
            // Before:3Teams, each Team has 4 people
            // After :4Teams, each Team has 3 people
            List<List<int>> result = _teamReform.DevideBeforeTeam2After(3, 4, 4, false);

            // List size check
            Assert.Equal(4, result.Count);
            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(3, result[i].Count);
            }

            // Contents check
            Assert.Equal(0, result[0][0]);
            Assert.Equal(0, result[1][0]);
            Assert.Equal(0, result[2][0]);
            Assert.Equal(0, result[3][0]);
            Assert.Equal(1, result[0][1]);
            Assert.Equal(1, result[1][1]);
            Assert.Equal(1, result[2][1]);
            Assert.Equal(1, result[3][1]);
            Assert.Equal(2, result[0][2]);
            Assert.Equal(2, result[1][2]);
            Assert.Equal(2, result[2][2]);
            Assert.Equal(2, result[3][2]);
        }

        [Fact]
        public void SimpleTroupDevide_12x4_to_16x3()
        {
            // Before:12Teams, each Team has 4 people
            // After :16Teams, each Team has 3 people
            List<List<int>> result = _teamReform.DevideBeforeTeam2After(12, 4, 16, false);

            // List size check
            Assert.Equal(16, result.Count);
            for (int i = 0; i < 16; i++)
            {
                Assert.Equal(3, result[i].Count);
            }

            // Contents check
            Assert.Equal(0, result[0][0]);
            Assert.Equal(0, result[1][0]);
            Assert.Equal(0, result[2][0]);
            Assert.Equal(0, result[3][0]);
            Assert.Equal(1, result[4][0]);
            Assert.Equal(1, result[5][0]);
            Assert.Equal(1, result[6][0]);
            Assert.Equal(1, result[7][0]);
            Assert.Equal(2, result[8][0]);
            Assert.Equal(2, result[9][0]);
            Assert.Equal(2, result[10][0]);
            Assert.Equal(2, result[11][0]);
            Assert.Equal(3, result[12][0]);
            Assert.Equal(3, result[13][0]);
            Assert.Equal(3, result[14][0]);
            Assert.Equal(3, result[15][0]);
            Assert.Equal(4, result[0][1]);
            Assert.Equal(4, result[1][1]);
            Assert.Equal(4, result[2][1]);
            Assert.Equal(4, result[3][1]);
            Assert.Equal(5, result[4][1]);
            Assert.Equal(5, result[5][1]);
            Assert.Equal(5, result[6][1]);
            Assert.Equal(5, result[7][1]);
            Assert.Equal(6, result[8][1]);
            // ...
            Assert.Equal(7, result[14][1]);
            Assert.Equal(7, result[15][1]);
            Assert.Equal(8, result[0][2]);
            Assert.Equal(8, result[1][2]);
            Assert.Equal(8, result[2][2]);
            Assert.Equal(8, result[3][2]);
            Assert.Equal(9, result[4][2]);
            Assert.Equal(9, result[5][2]);
            Assert.Equal(9, result[6][2]);
            Assert.Equal(9, result[7][2]);
            Assert.Equal(10, result[8][2]);
            Assert.Equal(10, result[9][2]);
            Assert.Equal(10, result[10][2]);
            Assert.Equal(10, result[11][2]);
            Assert.Equal(11, result[12][2]);
            Assert.Equal(11, result[13][2]);
            Assert.Equal(11, result[14][2]);
            Assert.Equal(11, result[15][2]);
        }

        [Fact]
        public void SimpleTroupDevide_3x4_to_2x6()
        {
            // Before:3Teams, each Team has 4 people
            // After :2Teams, each Team has 6 people
            List<List<int>> result = _teamReform.DevideBeforeTeam2After(3, 4, 2, false);

            // List size check
            Assert.Equal(2, result.Count);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(6, result[i].Count);
            }

            // Contents check
            Assert.Equal(0, result[0][0]);
            Assert.Equal(0, result[1][0]);
            Assert.Equal(0, result[0][1]);
            Assert.Equal(0, result[1][1]);
            Assert.Equal(1, result[0][2]);
            Assert.Equal(1, result[1][2]);
            Assert.Equal(1, result[0][3]);
            Assert.Equal(1, result[1][3]);
            Assert.Equal(2, result[0][4]);
            Assert.Equal(2, result[1][4]);
            Assert.Equal(2, result[0][5]);
            Assert.Equal(2, result[1][5]);
        }

        [Fact]
        public void SimpleTroupDevide_3x4_to_4x3_random()
        {
            // Before:3Teams, each Team has 4 people
            // After :4Teams, each Team has 3 people
            List<List<int>> result1 = _teamReform.DevideBeforeTeam2After(3, 4, 4, true);
            List<List<int>> result2 = _teamReform.DevideBeforeTeam2After(3, 4, 4, true);

            // List size check
            Assert.Equal(4, result1.Count);
            Assert.Equal(4, result2.Count);
            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(3, result1[i].Count);
                Assert.Equal(3, result2[i].Count);

                // every after team must not have same before team member
                Assert.False(result1[i][0] == result1[i][1]);
                Assert.False(result1[i][1] == result1[i][2]);
                Assert.False(result1[i][2] == result1[i][0]);
                Assert.False(result2[i][0] == result2[i][1]);
                Assert.False(result2[i][1] == result2[i][2]);
                Assert.False(result2[i][2] == result2[i][0]);
            }
        }
    }
}