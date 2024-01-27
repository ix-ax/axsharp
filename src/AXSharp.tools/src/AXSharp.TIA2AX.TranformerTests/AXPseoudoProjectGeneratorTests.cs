using Xunit;
using AXSharp.TIA2AX.Transformer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AXSharp.TIA2AX.Transformer.Tests
{
    public class AXPseoudoProjectGeneratorTests
    {
        [Fact()]
        public void CreateTest()
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);
            AXPseoudoProjectGenerator.Create(assemblyLocation.DirectoryName, 
               "output", 
               new string[] { Path.Combine(assemblyLocation.DirectoryName, "samples", "ExportViacDbBezInstancneho.db") }, new Options() { Namespace = "nmspc"});

            var expectedConfiguration = @"CONFIGURATION MyConfiguration
TASK Main(Interval := T#10ms, Priority := 1);
PROGRAM P1 WITH Main: MyProgram;
	VAR_GLOBAL
	{#ix-attr: [DBAttribute()]}
	DB_StorageNok : nmspc.DB_StorageNok;
	{#ix-attr: [DBAttribute()]}
	DBRivetingSetup : nmspc.DBRivetingSetup;
	{#ix-attr: [DBAttribute()]}
	DB_Storage : nmspc.DB_Storage;
	{#ix-attr: [DBAttribute()]}
	DbData : nmspc.DbData;
	END_VAR
END_CONFIGURATION
PROGRAM MyProgram
    VAR
        
    END_VAR
    ;
END_PROGRAM";
            var actualConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "output", "src", "configuration.st"));

            Assert.Equal(expectedConfiguration, actualConfiguration);

            var expectedTypes = @"NAMESPACE nmspc
 TYPE tGlobalData_22 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tGlobalData_21 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_20 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_19 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_18 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_17 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_16 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_15 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_14 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_13 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_4_12 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_3_11 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_10 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_9 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_3_8 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_2_7 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_1_6 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_5 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tdigital_4 :
STRUCT
         Required : Bool;
         Measured : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_3 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE tdata_2 :
STRUCT
         Required : String[128];
         Measured : String[128];
         StarNotationEnabled : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_1 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE tCheckDataAnalog_1 :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      analog : tanalog_1;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tHeaderResults :
   STRUCT
      InProgress : Bool;
      IsPassed : Bool;
      IsFailed : Bool;
      IsReset : Bool;
      IsRework : Bool;
      IsMaster : Bool;
      IsSkipped : Bool;
      wasManual : Bool;
      wasReset : Bool;
   END_STRUCT;

END_TYPE

TYPE tCheckDataData :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      data : tdata_2;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tCheckDataAnalog :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      analog : tanalog_1;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tCheckDataDigital :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      digital : tdigital_4;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tRivetingData :
   STRUCT
      RivetingExclusion : Bool;
      InitialHeightCheck : tCheckDataAnalog_1;
      InitialPulseLenght : Time;   // pociatocny pulz, nech sa urychli nahrievanie
      InitialPulseDutyCycle : Real;   // plnenie 0-100percent
      WorkingPulseLenghtMax : Time;   // pracovny kde sa tavi material, stop ked dosiahne potrebnu vysku
      WorkingPulseDutyCycle : Real;   // plnenie 0-100percent
      DeatachPulseDutyCycle : Real;   // plnenie 0-100percent
      PrePinCoolingStandBy : Time;
      PinCoolingTime : Time;
      SwitchOffHeatingHeight : Real;
      EndHeightCheck : tCheckDataAnalog_1;
      DeatachPulseTime : Time;   // odtrhove nahriatie taviacej ihly, aby neotrhlo hlavicku
      RivetingTimingCheck : Time;
      PostCooling : Time;
   END_STRUCT;

END_TYPE

TYPE tHeaderCu :
   STRUCT
      NextOnPassed : UInt;
      NextOnFailed : UInt;
   END_STRUCT;

END_TYPE

TYPE tCu20Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      DataCheck_Priklad_10 : tCheckDataData;
      GearAdjusterFront : tCheckDataDigital;
      GearAdjusterInputFront : tCheckDataDigital;
      GearAdjusterBack : tCheckDataDigital;
      GearAdjusterInputBack : tCheckDataDigital;
      StudBallBack : tCheckDataDigital;
      StudBallTop : tCheckDataDigital;
      SlideLevelingMotor : tCheckDataDigital;
      SlideHorizontal : tCheckDataDigital;
      GearAdjusterFrontPressingOK : tCheckDataDigital;
      GearAdjusterBackPressingOK : tCheckDataDigital;
      ScrewingBackStatus : tCheckDataDigital;
      ScrewingTopStatus : tCheckDataDigital;
      ScrewingBackTorque : tCheckDataAnalog;
      ScrewingTopTorque : tCheckDataAnalog;
      ScrewingBackAngle : tCheckDataAnalog;
      ScrewingTopAngle : tCheckDataAnalog;
      RivetingResult : tCheckDataDigital;
      InitialHeightCheck : tCheckDataAnalog;
      EndHeightCheck : tCheckDataAnalog;
      PresentPCB : tCheckDataDigital;
      PCB_DATA : tCheckDataData;
      HotRivetingActive : Bool;
      HotRivetingSetup : Int;
      HotRivetingData : tRivetingData;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tLightType :
   STRUCT
      ID_Type : String[32];
      ID_Part : String[32];     
      Type_empty : Bool;
      Type_Base : Bool;
      Type_Mid : Bool;
      Type_High : Bool;
      Type_4 : Bool;
      Type_5 : Bool;
      Type_6 : Bool;
      Type_7 : Bool;
      Type_8 : Bool;
      LHD : Bool;   // EU verzia svetla
      RHD : Bool;   // UK verzia svetla
      Muster : Bool;
      Rework : Bool;
      LH : Bool;
      RH : Bool;
      PartINStation : Bool;
   END_STRUCT;

END_TYPE

TYPE tHeader :
   STRUCT
      Identificator : String[50];
      Reference : String[50];
      NextStation : UInt;
      LastStation : UInt;
      OpenOnStation : UInt;
      Carrier : UInt;
      Results : tHeaderResults;
      Result : Byte;
      FailureCode : UDInt;
   END_STRUCT;

END_TYPE

TYPE tCu10Data :
   STRUCT
      Flow : tHeaderCu;
      LabelRefText : String[32];
      LabelDMC : String[32];
      SerialNO : String[8];     
      LabelName : String[16];   // nazovetikety v tlaciarni
      sufix : String[2];
      TempDMC : String[32];
      Label : tCheckDataData;
      SMI_Screewing_1 : tSMI_Screewing_1_6;

      SMI_Screewing_2 : tSMI_Screewing_2_7;

      SMI_Screewing_3 : tSMI_Screewing_3_8;

      SMI_Code : tCheckDataData;
      LDM_Screewing_1 : tLDM_Screewing_1_9;

      LDM_Screewing_2 : tLDM_Screewing_2_10;

      LDM_Screewing_3 : tLDM_Screewing_3_11;

      LDM_Screewing_4 : tLDM_Screewing_4_12;

      LDM_Code : tCheckDataData;
      HoleHigh : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu30Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      CameraCheckConnector : tCheckDataDigital;
      CameraJob : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu40Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      LDM_Screewing_1 : tLDM_Screewing_1_9;

      LDM_Screewing_2 : tLDM_Screewing_2_10;

      LDM_Code : tCheckDataData;
      SBL_Code : tCheckDataData;
      ReflectorProjector_Code : tCheckDataData;
      Reflector : tCheckDataDigital;
      BaseLDMModul : tCheckDataDigital;
      HighSBLModul : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu50Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      svetlopritomnost_15 : tCheckDataDigital;
      FTI_Code : tCheckDataData;
      CameraCheckConnector : tCheckDataDigital;
      CameraCheckGuma : tCheckDataDigital;
      CameraCheckLight : tCheckDataDigital;
      CameraJob : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu60Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlopritomnost : tCheckDataDigital;
      ATE_ResultTest : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu70Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu80Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
      CasOfukovania : Time;
   END_STRUCT;

END_TYPE

TYPE tTier :
   STRUCT
      FirstRun : Bool;
      Progress : UInt;
      PreviousProgress : UInt;
      CycleCounter : UInt;
      ActualCycleCounter : UInt;
      ProgressTime : Time;
      Message : String[50];
   END_STRUCT;

END_TYPE

TYPE tDataManager :
   STRUCT
      RequiredReference : String[50];
      PreviousReference : String[50];
      RequiredReferenceAck : String[50];
      ActiveStationNo : Int;
      HmiRequest : Int;
      ProcessingStatus : Byte;
      ReferenceLoadStart : Bool;
      ReferenceLoadDone : Bool;
      DataChangedOnHmi : Bool;
   END_STRUCT;

END_TYPE

TYPE tData :
   STRUCT
      EntityHeader : tHeader;
      LightType : tLightType;
      GlobalData : tGlobalData_21;

      Cu10 : tCu10Data;
      Cu20 : tCu20Data;
      Cu30 : tCu30Data;
      Cu40 : tCu40Data;
      Cu50 : tCu50Data;
      Cu60 : tCu60Data;
      Cu70 : tCu70Data;
      Cu80 : tCu80Data;
      ReferenceNo : Int;
   END_STRUCT;

END_TYPE

TYPE tHmiButtonMts :
   STRUCT
      Appearance : Int;
      ControlEnable : Bool;
      Visibility : Bool;
      ClickSetBit : Bool;
      ClickSwitchBit : Bool;
      PressReleaseBit : Bool;
   END_STRUCT;

END_TYPE

TYPE tData_bobock :
   STRUCT
      EntityHeader : tHeader;
      LightType : tLightType;
      GlobalData : tGlobalData_21;

      ReferenceNo : Int;
   END_STRUCT;

END_TYPE

TYPE tReference :
   STRUCT
      Manager : tDataManager;
      Data : tData;
      tier : tTier;
   END_STRUCT;

END_TYPE

{#ix-db: DB_StorageNok}
CLASS PUBLIC DB_StorageNok 
   VAR PUBLIC 
      Data : Array[0..2] of tData_bobock;
   END_VAR
END_CLASS


{#ix-db: DBRivetingSetup}
CLASS PUBLIC DBRivetingSetup 

   VAR PUBLIC 
      SadyParametrov : Array[0..20] of tRivetingData;
      SadaNaHmi : tRivetingData;
   END_VAR
   VAR PUBLIC 
      ButtonLoadFromData : tHmiButtonMts;
      ButtonSaveToData : tHmiButtonMts;
   END_VAR
   VAR PUBLIC 
      AcviteIndex : Int;
      AnalogRetainTareData : Array[0..10] of Real;
   END_VAR
END_CLASS


{#ix-db: DB_Storage}
CLASS PUBLIC DB_Storage 
   VAR PUBLIC 
      Data : Array[0..50] of tData_bobock;
   END_VAR
END_CLASS


{#ix-db: DbData}
CLASS PUBLIC DbData 

   VAR PUBLIC 
      Reference : Array[0..10] of tReference;
      Station : Array[0..10] of tData;
      Carrier : Array[0..10] of tData;
      CarrierAtStation : Array[0..10] of UInt;
   END_VAR
END_CLASS


 
END_NAMESPACE";


            var actualTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "output", "src", "ExportViacDbBezInstancneho.db.st"));


            Assert.Equal(expectedTypes.Split('\n').Length, actualTypes.Split('\n').Length);

            var exp = expectedTypes.Split('\n').Select(p => p.Trim()).ToArray();
            var act = actualTypes.Split('\n').Select(p => p.Trim()).ToArray();
            for (int i = 0; i < exp.Length; i++)
            {
                Assert.Equal(exp[i], act[i]);
            }

                
            

            
        }

        [Fact()]
        public void RunToolWithArguments()
        {
            var assemblyLocation = new FileInfo(Assembly.GetExecutingAssembly().Location);

            // Define the path to the executable file.
            string exePath = Path.Combine(assemblyLocation.DirectoryName, "AXSharp.TIA2AX.Transformer.exe");

            // Define the arguments to pass.
            // If the arguments might contain spaces, wrap them in quotes.
            string arguments = $"-o {assemblyLocation.DirectoryName} -d {Path.Combine(assemblyLocation.DirectoryName, "samples", "ExportViacDbBezInstancneho.db")}";

            try
            {
                // Set up the process start information.
                ProcessStartInfo startInfo = new ProcessStartInfo(exePath)
                {
                    Arguments = arguments, // Pass the arguments.
                    UseShellExecute = false, // Don't use the shell to start the process.
                    CreateNoWindow = true, // Don't create a new window.
                    RedirectStandardOutput = true, // Redirect standard output (if needed).
                    RedirectStandardError = true // Redirect standard error (if needed).
                };

                // Start the process with the info specified.
                using (Process process = Process.Start(startInfo))
                {
                    // Read the output (if you want to do something with it).
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Wait for the process to finish.
                    process.WaitForExit();

                    // Write the output to the console.
                    Console.WriteLine("Output:");
                    Console.WriteLine(output);

                    // Write any errors to the console.
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(error);
                    }
                    
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // An exception occurred, write the details.
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
                throw ex;
            }
               

            var expectedConfiguration = @"CONFIGURATION MyConfiguration
TASK Main(Interval := T#10ms, Priority := 1);
PROGRAM P1 WITH Main: MyProgram;
	VAR_GLOBAL
	{#ix-attr: [DBAttribute()]}
	DB_StorageNok : DB_StorageNok;
	{#ix-attr: [DBAttribute()]}
	DBRivetingSetup : DBRivetingSetup;
	{#ix-attr: [DBAttribute()]}
	DB_Storage : DB_Storage;
	{#ix-attr: [DBAttribute()]}
	DbData : DbData;
	END_VAR
END_CONFIGURATION
PROGRAM MyProgram
    VAR
        
    END_VAR
    ;
END_PROGRAM";
            var actualConfiguration = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "PseudoAX", "src", "configuration.st"));

            Assert.Equal(expectedConfiguration, actualConfiguration);

            var expectedTypes = @"TYPE tGlobalData_22 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tGlobalData_21 :
STRUCT
         TraceabilityActive : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_20 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_19 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_18 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_17 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_16 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_15 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_14 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tBasicType_13 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_4_12 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_3_11 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_2_10 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tLDM_Screewing_1_9 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_3_8 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_2_7 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tSMI_Screewing_1_6 :
STRUCT
         Status : tCheckDataDigital;
         Torque : tCheckDataAnalog;
         Angle : tCheckDataAnalog;
      END_STRUCT;
END_TYPE

TYPE tBasicType_5 :
STRUCT
         tempBool : Bool;
      END_STRUCT;
END_TYPE

TYPE tdigital_4 :
STRUCT
         Required : Bool;
         Measured : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_3 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE tdata_2 :
STRUCT
         Required : String[128];
         Measured : String[128];
         StarNotationEnabled : Bool;
      END_STRUCT;
END_TYPE

TYPE tanalog_1 :
STRUCT
         Min : Real;
         Measured : Real;
         Max : Real;
      END_STRUCT;
END_TYPE

TYPE tCheckDataAnalog_1 :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      analog : tanalog_1;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tHeaderResults :
   STRUCT
      InProgress : Bool;
      IsPassed : Bool;
      IsFailed : Bool;
      IsReset : Bool;
      IsRework : Bool;
      IsMaster : Bool;
      IsSkipped : Bool;
      wasManual : Bool;
      wasReset : Bool;
   END_STRUCT;

END_TYPE

TYPE tCheckDataData :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      data : tdata_2;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tCheckDataAnalog :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      analog : tanalog_1;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tCheckDataDigital :
   STRUCT
      IsExcluded : Bool;
      IsVisible : Bool;
      Typecheck : Int;   // 0=inactive, 1-logic,2-analog,3:data
      digital : tdigital_4;

      PassedTime : Time := T#50ms;
      FailedTime : Time := T#100ms;
      Result : Byte;
      NumberOfRetries : UInt;
      AllowedNumberOfRetries : UInt;
      SourceID : UInt;   // identifikator stanice/kroku/rezimu
   END_STRUCT;

END_TYPE

TYPE tRivetingData :
   STRUCT
      RivetingExclusion : Bool;
      InitialHeightCheck : tCheckDataAnalog_1;
      InitialPulseLenght : Time;   // pociatocny pulz, nech sa urychli nahrievanie
      InitialPulseDutyCycle : Real;   // plnenie 0-100percent
      WorkingPulseLenghtMax : Time;   // pracovny kde sa tavi material, stop ked dosiahne potrebnu vysku
      WorkingPulseDutyCycle : Real;   // plnenie 0-100percent
      DeatachPulseDutyCycle : Real;   // plnenie 0-100percent
      PrePinCoolingStandBy : Time;
      PinCoolingTime : Time;
      SwitchOffHeatingHeight : Real;
      EndHeightCheck : tCheckDataAnalog_1;
      DeatachPulseTime : Time;   // odtrhove nahriatie taviacej ihly, aby neotrhlo hlavicku
      RivetingTimingCheck : Time;
      PostCooling : Time;
   END_STRUCT;

END_TYPE

TYPE tHeaderCu :
   STRUCT
      NextOnPassed : UInt;
      NextOnFailed : UInt;
   END_STRUCT;

END_TYPE

TYPE tCu20Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      DataCheck_Priklad_10 : tCheckDataData;
      GearAdjusterFront : tCheckDataDigital;
      GearAdjusterInputFront : tCheckDataDigital;
      GearAdjusterBack : tCheckDataDigital;
      GearAdjusterInputBack : tCheckDataDigital;
      StudBallBack : tCheckDataDigital;
      StudBallTop : tCheckDataDigital;
      SlideLevelingMotor : tCheckDataDigital;
      SlideHorizontal : tCheckDataDigital;
      GearAdjusterFrontPressingOK : tCheckDataDigital;
      GearAdjusterBackPressingOK : tCheckDataDigital;
      ScrewingBackStatus : tCheckDataDigital;
      ScrewingTopStatus : tCheckDataDigital;
      ScrewingBackTorque : tCheckDataAnalog;
      ScrewingTopTorque : tCheckDataAnalog;
      ScrewingBackAngle : tCheckDataAnalog;
      ScrewingTopAngle : tCheckDataAnalog;
      RivetingResult : tCheckDataDigital;
      InitialHeightCheck : tCheckDataAnalog;
      EndHeightCheck : tCheckDataAnalog;
      PresentPCB : tCheckDataDigital;
      PCB_DATA : tCheckDataData;
      HotRivetingActive : Bool;
      HotRivetingSetup : Int;
      HotRivetingData : tRivetingData;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tLightType :
   STRUCT
      ID_Type : String[32];
      ID_Part : String[32];     
      Type_empty : Bool;
      Type_Base : Bool;
      Type_Mid : Bool;
      Type_High : Bool;
      Type_4 : Bool;
      Type_5 : Bool;
      Type_6 : Bool;
      Type_7 : Bool;
      Type_8 : Bool;
      LHD : Bool;   // EU verzia svetla
      RHD : Bool;   // UK verzia svetla
      Muster : Bool;
      Rework : Bool;
      LH : Bool;
      RH : Bool;
      PartINStation : Bool;
   END_STRUCT;

END_TYPE

TYPE tHeader :
   STRUCT
      Identificator : String[50];
      Reference : String[50];
      NextStation : UInt;
      LastStation : UInt;
      OpenOnStation : UInt;
      Carrier : UInt;
      Results : tHeaderResults;
      Result : Byte;
      FailureCode : UDInt;
   END_STRUCT;

END_TYPE

TYPE tCu10Data :
   STRUCT
      Flow : tHeaderCu;
      LabelRefText : String[32];
      LabelDMC : String[32];
      SerialNO : String[8];     
      LabelName : String[16];   // nazovetikety v tlaciarni
      sufix : String[2];
      TempDMC : String[32];
      Label : tCheckDataData;
      SMI_Screewing_1 : tSMI_Screewing_1_6;

      SMI_Screewing_2 : tSMI_Screewing_2_7;

      SMI_Screewing_3 : tSMI_Screewing_3_8;

      SMI_Code : tCheckDataData;
      LDM_Screewing_1 : tLDM_Screewing_1_9;

      LDM_Screewing_2 : tLDM_Screewing_2_10;

      LDM_Screewing_3 : tLDM_Screewing_3_11;

      LDM_Screewing_4 : tLDM_Screewing_4_12;

      LDM_Code : tCheckDataData;
      HoleHigh : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu30Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      CameraCheckConnector : tCheckDataDigital;
      CameraJob : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu40Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      LDM_Screewing_1 : tLDM_Screewing_1_9;

      LDM_Screewing_2 : tLDM_Screewing_2_10;

      LDM_Code : tCheckDataData;
      SBL_Code : tCheckDataData;
      ReflectorProjector_Code : tCheckDataData;
      Reflector : tCheckDataDigital;
      BaseLDMModul : tCheckDataDigital;
      HighSBLModul : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu50Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlovyska : tCheckDataAnalog;
      svetlopritomnost : tCheckDataDigital;
      svetlopritomnost_15 : tCheckDataDigital;
      FTI_Code : tCheckDataData;
      CameraCheckConnector : tCheckDataDigital;
      CameraCheckGuma : tCheckDataDigital;
      CameraCheckLight : tCheckDataDigital;
      CameraJob : Int;
      CameraExcluded : Bool;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu60Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      svetlopritomnost : tCheckDataDigital;
      ATE_ResultTest : tCheckDataDigital;
      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu70Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
   END_STRUCT;

END_TYPE

TYPE tCu80Data :
   STRUCT
      Flow : tHeaderCu;
      BasicType : tBasicType_5;

      DummyCheckDigital : tCheckDataDigital;   // DigitalCheckData
      DummyCheckAnalog : tCheckDataAnalog;   // AnalogCheckData
      DummyCheckData : tCheckDataData;   // DataCheckData
      CasOfukovania : Time;
   END_STRUCT;

END_TYPE

TYPE tTier :
   STRUCT
      FirstRun : Bool;
      Progress : UInt;
      PreviousProgress : UInt;
      CycleCounter : UInt;
      ActualCycleCounter : UInt;
      ProgressTime : Time;
      Message : String[50];
   END_STRUCT;

END_TYPE

TYPE tDataManager :
   STRUCT
      RequiredReference : String[50];
      PreviousReference : String[50];
      RequiredReferenceAck : String[50];
      ActiveStationNo : Int;
      HmiRequest : Int;
      ProcessingStatus : Byte;
      ReferenceLoadStart : Bool;
      ReferenceLoadDone : Bool;
      DataChangedOnHmi : Bool;
   END_STRUCT;

END_TYPE

TYPE tData :
   STRUCT
      EntityHeader : tHeader;
      LightType : tLightType;
      GlobalData : tGlobalData_21;

      Cu10 : tCu10Data;
      Cu20 : tCu20Data;
      Cu30 : tCu30Data;
      Cu40 : tCu40Data;
      Cu50 : tCu50Data;
      Cu60 : tCu60Data;
      Cu70 : tCu70Data;
      Cu80 : tCu80Data;
      ReferenceNo : Int;
   END_STRUCT;

END_TYPE

TYPE tHmiButtonMts :
   STRUCT
      Appearance : Int;
      ControlEnable : Bool;
      Visibility : Bool;
      ClickSetBit : Bool;
      ClickSwitchBit : Bool;
      PressReleaseBit : Bool;
   END_STRUCT;

END_TYPE

TYPE tData_bobock :
   STRUCT
      EntityHeader : tHeader;
      LightType : tLightType;
      GlobalData : tGlobalData_21;

      ReferenceNo : Int;
   END_STRUCT;

END_TYPE

TYPE tReference :
   STRUCT
      Manager : tDataManager;
      Data : tData;
      tier : tTier;
   END_STRUCT;

END_TYPE
CLASS PUBLIC DB_StorageNok 
   VAR PUBLIC 
      Data : Array[0..2] of tData_bobock;
   END_VAR
END_CLASS
CLASS PUBLIC DBRivetingSetup 

   VAR PUBLIC 
      SadyParametrov : Array[0..20] of tRivetingData;
      SadaNaHmi : tRivetingData;
   END_VAR
   VAR PUBLIC 
      ButtonLoadFromData : tHmiButtonMts;
      ButtonSaveToData : tHmiButtonMts;
   END_VAR
   VAR PUBLIC 
      AcviteIndex : Int;
      AnalogRetainTareData : Array[0..10] of Real;
   END_VAR
END_CLASS
CLASS PUBLIC DB_Storage 
   VAR PUBLIC 
      Data : Array[0..50] of tData_bobock;
   END_VAR
END_CLASS
CLASS PUBLIC DbData 

   VAR PUBLIC 
      Reference : Array[0..10] of tReference;
      Station : Array[0..10] of tData;
      Carrier : Array[0..10] of tData;
      CarrierAtStation : Array[0..10] of UInt;
   END_VAR
END_CLASS


";

            var actualTypes = File.ReadAllText(Path.Combine(assemblyLocation.DirectoryName, "PseudoAX", "src", "ExportViacDbBezInstancneho.db.st"));

            Assert.Equal(expectedTypes, actualTypes);
        }
    }
}