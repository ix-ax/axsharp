// Ix.RenderableContent.Tests
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/ix/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/ix/blob/master/LICENSE
// Third party licenses: https://github.com/ix-ax/ix/blob/master/notices.md

using Ix.Presentation.Blazor.Controls.RenderableContent;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Components.Rendering;
using Xunit;

namespace Ix.RenderableContent.Tests
{
    public class ComponentServiceTests : IClassFixture<RenderableContentTestsFixture>
    {
        private RenderableContentTestsFixture _fixture;
        private string _display = "Display";
        private string _control = "Control";
        private string _shadowDisplay = "ShadowDisplay";
        private string _shadowControl = "ShadowControl";
        public ComponentServiceTests(RenderableContentTestsFixture fixture)
        {
            this._fixture = fixture;
        }


        //GenericComponents
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BaseGenericControlComponent_NotNullAreEqual()
        {
            //Arrange
            Type typeArg = typeof(int);
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetGenericComponent("OnlinerBaseControlView`1", typeArg);
            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);
        }

        [Fact]
        public void Get_BaseGenericIntDisplayComponent_NotNullAreEqual()
        {
            //Arrange
            Type typeArg = typeof(int);
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetGenericComponent("OnlinerBaseDisplayView`1", typeArg);
            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseDisplayView`1", component.GetType().Name);
        }
        //-----------------------------------------------------------------//
        //BaseComponents Display
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerBool";
            var buildedComponentName = $"{primitiveTypeName}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDateTime";
            var buildedComponentName = $"{primitiveTypeName}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDate";
            var buildedComponentName = $"{primitiveTypeName}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_TimeOfDayDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerTimeOfDay";
            var buildedComponentName = $"{primitiveTypeName}{_display}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        //-----------------------------------------------------------------//
        //BaseComponents Control
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerBool";
            var buildedComponentName = $"{primitiveTypeName}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDateTime";
            var buildedComponentName = $"{primitiveTypeName}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDate";
            var buildedComponentName = $"{primitiveTypeName}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_TimeOfDayControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerTimeOfDay";
            var buildedComponentName = $"{primitiveTypeName}{_control}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        //-----------------------------------------------------------------//
        //BaseComponents ShadowDisplay
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerBool";
            var buildedComponentName = $"{primitiveTypeName}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDateTime";
            var buildedComponentName = $"{primitiveTypeName}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDate";
            var buildedComponentName = $"{primitiveTypeName}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_TimeOfDayShadowDisplayView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerTimeOfDay";
            var buildedComponentName = $"{primitiveTypeName}{_shadowDisplay}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        //-----------------------------------------------------------------//
        //BaseComponents ShadowControl
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_BoolShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerBool";
            var buildedComponentName = $"{primitiveTypeName}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDateTime";
            var buildedComponentName = $"{primitiveTypeName}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_DateShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerDate";
            var buildedComponentName = $"{primitiveTypeName}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_TimeOfDayShadowControlView_NotNullAreEqual()
        {
            //Arrange
            var primitiveTypeName = "OnlinerTimeOfDay";
            var buildedComponentName = $"{primitiveTypeName}{_shadowControl}View";
            //Act
            var component = _fixture.RenderableContent.ComponentService.GetBaseComponent(buildedComponentName);
            //Assert
            Assert.NotNull(component);
            Assert.Equal(buildedComponentName, component.GetType().Name);

        }
        [Fact]
        public void Get_BoolControlComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testBool.GetType();
            var name = testedObjType.Name;

            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _control, _fixture.RenderableContent.ComponentService.GetBaseComponent);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBoolControlView", component.GetType().Name);

        }

        [Fact]
        public void Get_BoolDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testBool.GetType();
            var name = testedObjType.Name;


            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _display, _fixture.RenderableContent.ComponentService.GetBaseComponent);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBoolDisplayView", component.GetType().Name);

        }
        [Fact]
        public void Get_DateTimeDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            var name = testedObjType.Name;


            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, _display, _fixture.RenderableContent.ComponentService.GetBaseComponent);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerDateTimeDisplayView", component.GetType().Name);

        }
        [Fact]
        public void Get_ComponentWithinPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            var name = testedObjType.Name;


            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, "Base-Display", _fixture.RenderableContent.ComponentService.GetBaseComponent);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerDateTimeDisplayView", component.GetType().Name);

        }

        [Fact]
        public void Get_ComponentWithinPipelineRandomString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            var name = testedObjType.Name;
            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, "Bases-Displays", _fixture.RenderableContent.ComponentService.GetBaseComponent);
            //Assert
            Assert.Null(component);


        }
        [Fact]
        public void Get_ComponentWithinPipelineEmptyString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.TestDateTime.GetType();
            var name = testedObjType.Name;
            //Act
            var component = _fixture.RenderableContent.ViewLocatorBuilder(testedObjType, "", _fixture.RenderableContent.ComponentService.GetBaseComponent);
            //Assert
            Assert.Null(component);

        }
        //-----------------------------------------------------------------//
        //ViewGenericLocatorBuilder
        //-----------------------------------------------------------------//
        [Fact]
        public void Get_GenericBaseControlComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = false;

            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, _control, typeArg, isEnum);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);

        }
        [Fact]
        public void Get_GenericBaseDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = false;

            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, _display, typeArg, isEnum);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseDisplayView`1", component.GetType().Name);

        }
        [Fact]
        public void Get_GenericBaseControlWithinPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testPrimitive.testInteger.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = false;

            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, "Base-Manual-Service-Control", typeArg, isEnum);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("OnlinerBaseControlView`1", component.GetType().Name);
        }

        [Fact]
        public void Get_GenericEnumControlComponentWithBuilderPipeline_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = true;

            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, "ahoj-cau-control", typeArg, isEnum);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("EnumeratorContainerControlView`1", component.GetType().Name);

        }

        [Fact]
        public void Get_GenericEnumDisplayComponentWithBuilder_NotNullAreEqual()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = true;

            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, _display, typeArg, isEnum);

            //Assert
            Assert.NotNull(component);
            Assert.Equal("EnumeratorContainerDisplayView`1", component.GetType().Name);

        }

        [Fact]
        public void Get_GenericEnumRandomString_Unsuccessfull()
        {
            //Arrange
            var testedObjType = _fixture.Connector.testingProgram.testEnum.GetType();
            var genericName = testedObjType.BaseType.BaseType.Name;
            var typeArg = testedObjType.BaseType.GenericTypeArguments[0];
            var isEnum = true;
            //Act
            var component = _fixture.RenderableContent.ViewGenericLocatorBuilder(genericName, "fadfa=-faf", typeArg, isEnum);
            //Assert
            Assert.Null(component);

        }
        //-----------------------------------------------------------------//

    }
}