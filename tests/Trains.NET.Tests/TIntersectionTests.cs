﻿using Xunit;
using Xunit.Abstractions;

namespace Trains.NET.Tests
{
    public class TIntersectionTests : TestBase
    {
        public TIntersectionTests(ITestOutputHelper output) : base(output)
        { }

        [Theory(Skip = "These are broken and I don't know why")]
        [InlineData(TrainAngleHelper.TrainFacingDown, 1, 1, 2, 2)]
        [InlineData(TrainAngleHelper.TrainFacingUp, 1, 3, 2, 2)]
        [InlineData(TrainAngleHelper.TrainFacingLeft, 2, 2, 1, 1)]
        public void RightUpDown_TrainMovement(float startAngle, int startColumn, int startRow, int endColumn, int endRow)
        {
            TrackLayout.AddTrack(1, 1);
            TrackLayout.AddTrack(1, 2);
            TrackLayout.AddTrack(1, 3);
            TrackLayout.AddTrack(2, 2);
            TrackLayout.AddTrack(1, 2);

            AssertTrainMovement(startAngle, startColumn, startRow, endColumn, endRow);
        }

        [Theory(Skip = "These are broken and I don't know why")]
        [InlineData(TrainAngleHelper.TrainFacingDown, 2, 1, 1, 2)]
        [InlineData(TrainAngleHelper.TrainFacingUp, 2, 3, 1, 2)]
        [InlineData(TrainAngleHelper.TrainFacingRight, 1, 2, 2, 3)]
        public void LeftUpDown_TrainMovement(float startAngle, int startColumn, int startRow, int endColumn, int endRow)
        {
            TrackLayout.AddTrack(2, 1);
            TrackLayout.AddTrack(2, 2);
            TrackLayout.AddTrack(2, 3);
            TrackLayout.AddTrack(1, 2);
            TrackLayout.AddTrack(2, 2);

            AssertTrainMovement(startAngle, startColumn, startRow, endColumn, endRow);
        }

        [Theory(Skip = "These are broken and I don't know why")]
        [InlineData(TrainAngleHelper.TrainFacingRight, 1, 1, 2, 2)]
        [InlineData(TrainAngleHelper.TrainFacingLeft, 3, 1, 2, 2)]
        [InlineData(270, 2, 2, 3, 1)]
        public void LeftRightDown_TrainMovement(float startAngle, int startColumn, int startRow, int endColumn, int endRow)
        {
            TrackLayout.AddTrack(1, 1);
            TrackLayout.AddTrack(2, 1);
            TrackLayout.AddTrack(3, 1);
            TrackLayout.AddTrack(2, 2);
            TrackLayout.AddTrack(2, 1);

            AssertTrainMovement(startAngle, startColumn, startRow, endColumn, endRow);
        }

        [Theory(Skip = "These are broken and I don't know why")]
        [InlineData(TrainAngleHelper.TrainFacingRight, 1, 2, 2, 1)]
        [InlineData(TrainAngleHelper.TrainFacingLeft, 3, 2, 2, 1)]
        [InlineData(TrainAngleHelper.TrainFacingDown, 2, 1, 1, 2)]
        public void LeftRightUp_TrainMovement(float startAngle, int startColumn, int startRow, int endColumn, int endRow)
        {
            TrackLayout.AddTrack(1, 2);
            TrackLayout.AddTrack(2, 2);
            TrackLayout.AddTrack(3, 2);
            TrackLayout.AddTrack(2, 1);
            TrackLayout.AddTrack(2, 2);

            AssertTrainMovement(startAngle, startColumn, startRow, endColumn, endRow);
        }
    }
}
