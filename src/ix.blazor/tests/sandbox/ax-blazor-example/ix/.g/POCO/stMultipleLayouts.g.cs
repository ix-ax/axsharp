using System;

namespace Pocos
{
    public partial class stMultipleLayouts
    {
        public TestStructOneGroupWithLayout Servo_S5 { get; set; } = new TestStructOneGroupWithLayout();
        public TestStruct Servo_S6 { get; set; } = new TestStruct();
        public TestStructMultipleGroups Servo_S7 { get; set; } = new TestStructMultipleGroups();
        public TestStructWithMainLayout Servo_S8 { get; set; } = new TestStructWithMainLayout();
        public string Piston_A1 { get; set; } = string.Empty;
        public string Piston_A2 { get; set; } = string.Empty;
        public string Piston_A3 { get; set; } = string.Empty;
        public string Piston_A4 { get; set; } = string.Empty;
        public Int16 Piston_A21 { get; set; }

        public Int16 Piston_A22 { get; set; }

        public Int16 Piston_A23 { get; set; }

        public Int16 Piston_A24 { get; set; }

        public Single Piston_A31 { get; set; }

        public Single Piston_A32 { get; set; }

        public Single Piston_A33 { get; set; }

        public Single Piston_A34 { get; set; }

        public Boolean Piston_A41 { get; set; }

        public Boolean Piston_A42 { get; set; }

        public Boolean Piston_A43 { get; set; }

        public Boolean Piston_A44 { get; set; }

        public string Servo_S1 { get; set; } = string.Empty;
        public string Servo_S2 { get; set; } = string.Empty;
        public IxComponent component { get; set; } = new IxComponent();
        public stTest3 Servo_S3 { get; set; } = new stTest3();
        public stTest3 Servo_S4 { get; set; } = new stTest3();
    }
}