﻿using System;
using Moq;
using NUnit.Framework;
using Rudder;
using Rudder.Wrappers;
using UnityEngine;

namespace Tests.EditMode
{
    public class RudderScreenPositionHandlerTests
    {
        [Test]
        public void Should_ReturnRudderTransformPosition_When_RenderModeIsScreenSpaceOverlay()
        {
            // ARRANGE
            var rudder = new Mock<IRudder>();
            var canvas = new Mock<IGetRenderMode>();
            var camera = new Mock<IGetWorldToScreenPoint>();
            var screenPositionHandler = new RudderScreenPositionHandler(camera.Object, canvas.Object);
            var expected = new Vector3(42, 42, 42);

            rudder.Setup(r => r.Position).Returns(expected);
            canvas.Setup(c => c.RenderMode).Returns(RenderMode.ScreenSpaceOverlay);

            // ACT
            var actual = screenPositionHandler.GetPosition(rudder.Object.Position);
            
            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_ReturnWorldToScreenPointForRudder_When_RenderModeIsScreenSpaceCamera()
        {
            // ARRANGE
            var rudder = new Mock<IRudder>();
            var canvas = new Mock<IGetRenderMode>();
            var camera = new Mock<IGetWorldToScreenPoint>();
            var screenPositionHandler = new RudderScreenPositionHandler(camera.Object, canvas.Object);
            
            var position = new Vector3(42, 42, 42);
            var expected = position * (float)Math.PI;

            camera.Setup(c => c.WorldToScreenPoint(position)).Returns(expected);

            rudder.Setup(r => r.Position).Returns(position);
            canvas.Setup(c => c.RenderMode).Returns(RenderMode.ScreenSpaceCamera);

            // ACT
            var actual = screenPositionHandler.GetPosition(rudder.Object.Position);
            
            // ASSERT
            Assert.AreEqual(expected, actual);
        }
    }
}