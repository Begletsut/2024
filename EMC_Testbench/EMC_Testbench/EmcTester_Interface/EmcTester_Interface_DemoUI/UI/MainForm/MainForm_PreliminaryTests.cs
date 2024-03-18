
using System;
using System.Windows.Forms;

using CAM_CANdriver_2;
using EmcTester_Interface_DemoUI.UI.Forms_PriliminaryTest;

namespace EmcTester_Interface_DemoUI
{

    // MainForm Emulator
    public partial class MainForm : Form
    {

        // private const byte PRILIMINARY_TEST_CAN_ID_POWER 0x11000001
        // private const byte PRILIMINARY_TEST_CAN_ID_ADC 0x11000002
        private const UInt32 PRILIMINARY_TEST_CAN_ID_POWER = 0x11000001;
        private const UInt32 PRILIMINARY_TEST_CAN_ID_ADC = 0x11000002;


        /*
        Data format in EMC tester commands:

        From Master (PC):	 00EFXX00: source address: 00, destination address: from 0x01 to 0xFF (00: to ALL devices)
        From Slaves (Testers):	 04EF00XX: source address: from 0x01 to 0xFF, destination address: 00
        Priority from master is 0, first byte in command Id: 0x00;
        Priority from slaves is 1 (first byte in command Id: 0x04).


        Digital IO
        App	Command name	Source	DLC	B0	B1	B2	B3	B4	B5	B6	B7
            Power off	Master	2	01	00						
            Power on	Master	2	01	01						
            Start test	Master	2	01	02						
            I am here (every 100ms)
        eTesterState_Ready	Slave	0								
            Power off ok
        eTesterState_EMC_PowerOff	Slave	2	Tester
        state	00						
            Power on ok
        eTesterState_EMC_PowerOn	Slave	2		01						
            Test state (every 100 ms)
        eTesterState_EMC_TestStarted
        eTesterState_EMC_TestFinished	Slave	> 0		TBD: it depends on the context

        typedef enum
        {
            eTesterState_NotInit,
            eTesterState_Ready,			// after Tester power On
            eTesterState_EMC_PowerOff, 
            eTesterState_EMC_PowerOn,		// after EMC power On, see IsConnected
            eTesterState_EMC_TestStarted, 		// after EMC test started, see eTestStage
            eTesterState_EMC_TestFinished,		// after EMC test finished, see eTestResult
        } eTesterState;
         */

        private const byte TESTER_PRIORITY_MASTER = 0x00;     // CAN priority: 0..6
        private const byte TESTER_PRIORITY_SLAVE = 0x01;     // CAN priority: 0..6
        private const byte TESTER_PGN = 0xFE;                 // User PGN
        private const byte TESTER_ADDRESS_MASTER = 0x00;     // Master address: 0
                                                             // #warning TODO: CHANGED, unique to tester
        private const byte TESTER_ADDRESS_SLAVE = 0x01;     // Slave address, Tester Id = 1..255 (0: to all)

        private const UInt32 TESTER_CAN_ID_FromMaster = ((TESTER_PRIORITY_MASTER << 26) | (TESTER_PGN << 16) | (TESTER_ADDRESS_MASTER << 8) | (TESTER_ADDRESS_SLAVE));
        private const UInt32 TESTER_CAN_MASK_FromSlave = 0x1FFF00FF;
        private const UInt32 TESTER_CAN_ID_FromSlave = ((TESTER_PRIORITY_MASTER << 26) | (TESTER_PGN << 16) | /* (TESTER_ADDRESS_SLAVE << 8) | */ (TESTER_ADDRESS_MASTER));
        private const UInt32 TESTER_CAN_RECV_timeout_ms = 1000;


        FormPreliminaryTest_ADC_Grid FormPriliminaryTest_ADC_Grid = new FormPreliminaryTest_ADC_Grid();
        FormPreliminaryTest_ADC_Log FormPriliminaryTest_ADC_Log = new FormPreliminaryTest_ADC_Log();

        private bool PreliminaryTests_ReceiveHandler(CAN_DATA_STRUC aCanMsg)
        {
            if (aCanMsg.id == PRILIMINARY_TEST_CAN_ID_ADC)
            {
                if ((aCanMsg.data != null) && (aCanMsg.data.Length == 8))
                {
                    FormPriliminaryTest_ADC_Grid.NewValueADC(aCanMsg.data[0], aCanMsg.data[1] == 1, (Int16)((aCanMsg.data[2] << 8) + aCanMsg.data[3]));
                    FormPriliminaryTest_ADC_Log.NewValueADC(aCanMsg.data[0], aCanMsg.data[1] == 1, (Int16)((aCanMsg.data[2] << 8) + aCanMsg.data[3]));
                    return true;
                }
            }
            else //if (aCanMsg.id == PRILIMINARY_TEST_CAN_ID_ADC)
            {
                if ((aCanMsg.data != null) && (aCanMsg.data.Length == 8))
                {
                    FormPriliminaryTest_ADC_Grid.NewValueADC(aCanMsg.data[1], true, (Int16)((aCanMsg.data[2] << 8) + aCanMsg.data[3]));
                    FormPriliminaryTest_ADC_Log.NewValueADC(aCanMsg.data[1], true, (Int16)((aCanMsg.data[2] << 8) + aCanMsg.data[3]));
                    return true;
                }
            }
            return false;
        }

        private void PreliminaryTests_ADC_Grid()
        {
            FormPriliminaryTest_ADC_Grid.Hide();
            FormPriliminaryTest_ADC_Grid.Show();
        }

        private void PreliminaryTests_ADC_Log()
        {
            FormPriliminaryTest_ADC_Log.Hide();
            FormPriliminaryTest_ADC_Log.Show();
        }

    } // class

} // namespace
