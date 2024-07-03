#region Copyright (c) 2024, Jack Leighton
// /////     __________________________________________________________________________________________________________________
// /////
// /////                  __                   __              __________                                      __   
// /////                _/  |_  ____   _______/  |_  __________\______   \_______   ____   ______ ____   _____/  |_ 
// /////                \   __\/ __ \ /  ___/\   __\/ __ \_  __ \     ___/\_  __ \_/ __ \ /  ___// __ \ /    \   __\
// /////                 |  | \  ___/ \___ \  |  | \  ___/|  | \/    |     |  | \/\  ___/ \___ \\  ___/|   |  \  |  
// /////                 |__|  \___  >____  > |__|  \___  >__|  |____|     |__|    \___  >____  >\___  >___|  /__|  
// /////                           \/     \/            \/                             \/     \/     \/     \/      
// /////                                                          .__       .__  .__          __                    
// /////                               ____________   ____   ____ |__|____  |  | |__| _______/  |_                  
// /////                              /  ___/\____ \_/ __ \_/ ___\|  \__  \ |  | |  |/  ___/\   __\                 
// /////                              \___ \ |  |_> >  ___/\  \___|  |/ __ \|  |_|  |\___ \  |  |                   
// /////                             /____  >|   __/ \___  >\___  >__(____  /____/__/____  > |__|                   
// /////                                  \/ |__|        \/     \/        \/             \/                         
// /////                                  __                         __  .__                                        
// /////                   _____   __ ___/  |_  ____   _____   _____/  |_|__|__  __ ____                            
// /////                   \__  \ |  |  \   __\/  _ \ /     \ /  _ \   __\  \  \/ // __ \                           
// /////                    / __ \|  |  /|  | (  <_> )  Y Y  (  <_> )  | |  |\   /\  ___/                           
// /////                   (____  /____/ |__|  \____/|__|_|  /\____/|__| |__| \_/  \___  >                          
// /////                        \/                         \/                          \/                           
// /////                                                  .__          __  .__                                      
// /////                                       __________ |  |  __ ___/  |_|__| ____   ____   ______                
// /////                                      /  ___/  _ \|  | |  |  \   __\  |/  _ \ /    \ /  ___/                
// /////                                      \___ (  <_> )  |_|  |  /|  | |  (  <_> )   |  \\___ \                 
// /////                                     /____  >____/|____/____/ |__| |__|\____/|___|  /____  >                
// /////                                          \/                                      \/     \/                 
// /////                                   Tester Present Specialist Automotive Solutions
// /////     __________________________________________________________________________________________________________________
// /////      |--------------------------------------------------------------------------------------------------------------|
// /////      |       https://github.com/jakka351/| https://testerPresent.com.au | https://facebook.com/testerPresent        |
// /////      |--------------------------------------------------------------------------------------------------------------|
// /////      | Copyright (c) 2022/2023/2024 Benjamin Jack Leighton                                                          |          
// /////      | All rights reserved.                                                                                         |
// /////      |--------------------------------------------------------------------------------------------------------------|
// /////        Redistribution and use in source and binary forms, with or without modification, are permitted provided that
// /////        the following conditions are met:
// /////        1.    With the express written consent of the copyright holder.
// /////        2.    Redistributions of source code must retain the above copyright notice, this
// /////              list of conditions and the following disclaimer.
// /////        3.    Redistributions in binary form must reproduce the above copyright notice, this
// /////              list of conditions and the following disclaimer in the documentation and/or other
// /////              materials provided with the distribution.
// /////        4.    Neither the name of the organization nor the names of its contributors may be used to
// /////              endorse or promote products derived from this software without specific prior written permission.
// /////      _________________________________________________________________________________________________________________
// /////      THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
// /////      INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// /////      DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// /////      SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// /////      SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// /////      WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
// /////      USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// /////      _________________________________________________________________________________________________________________
// /////
// /////       This software can only be distributed with my written permission. It is for my own educational purposes and  
// /////       is potentially dangerous to ECU health and safety. Gracias a Gato Blancoford desde las alturas del mar de chelle.                                                        
// /////      _________________________________________________________________________________________________________________
// /////
// /////
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endregion License
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//6.3.1 DTC Classifications
//There are two classifications of DTCs that an ECU shall support:
//• Continuous DTCs
//• On-demand DTCs, see Section 10.4 for more information regarding on-demand DTCs.
// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using J2534;
namespace FGCOM
{
	public partial class Orion
	{
		/////////////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
		//
		//   ██████╗██╗     ███████╗ █████╗ ██████╗     ██████╗ ████████╗ ██████╗
		//  ██╔════╝██║     ██╔════╝██╔══██╗██╔══██╗    ██╔══██╗╚══██╔══╝██╔════╝
		//  ██║     ██║     █████╗  ███████║██████╔╝    ██║  ██║   ██║   ██║     
		//  ██║     ██║     ██╔══╝  ██╔══██║██╔══██╗    ██║  ██║   ██║   ██║     
		//  ╚██████╗███████╗███████╗██║  ██║██║  ██║    ██████╔╝   ██║   ╚██████╗
		//   ╚═════╝╚══════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝    ╚═════╝    ╚═╝    ╚═════╝                                                                       
		// s
		/////////////////////////////////////////////////////////////////////////////
		/// CLEART DIAGNOSTIC TROUBLE CODES TOP MENU ITEM FUNCTION REQUESTS THE CURRENTLY SELECTED ECU TO CLEAR FAULT CODES
		/// SERVICE 0x14 CLEAR DTC - PSR 0x54 NRC 0x7F
		/// Parses the response and instructs the operator on the outcome need to provide direction on appropriate action for operator in case of
		/// a negative response.
		/// ////////////////////////////////////
		/// SID CLEAR DTC:                  0x14
		/// SID READ  DTC:                  0x15 
		/// SID CONT CODE:                  0x22
		/// DID LOCATION FOR CONT. CODES: 0xE6F3
		////////////////////////////////////////
		/// CAN LOG:
		///   (1662865757.573871) can0 726#0322E6F300000000
		///   (1662865757.582868) can0 72E#0462E6F30C000000
		///   (1662865757.586310) can0 726#0322020000000000
		///   (1662865757.592949) can0 72E#0462020000000000
		///   (1662865757.599693) can0 720#0322E6F300000000
		///   (1662865757.602675) can0 728#0462E6F30A000000
		///   (1662865757.605856) can0 720#0322020000000000
		///   (1662865757.612718) can0 728#0462020000000000
		///   (1662865759.195110) can0 726#0314FF0000000000
		///   (1662865759.204603) can0 72E#0354FF0000000000
		///   (1662865760.531975) can0 720#0314FF0000000000
		///   (1662865760.562499) can0 728#0354FF0000000000
		///   (1662865764.141895) can0 726#041800FF00000000
		///   (1662865764.149996) can0 72E#0258000000000000
		///   (1662865764.439037) can0 720#041800FF00000000
		///   (1662865764.442910) can0 728#0258000000000000
		/////////////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
		void DTCT()
		{
			InitializeComponent();
			this.progressBarDtc.Value = 0;
			//timerDtc.Enabled = true;
		}
		
		private async void readBarDtc()
		{
        	labelDtc1.Visible = false;
			labelDtc2.Visible = false;
			progressBarDtc.Value = 0;
			timerDtc2.Enabled = true;
		}
		/////////////////////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
		private async void clearBarDtc()
		{
        	labelDtc1.Visible = false;
			labelDtc2.Visible = false;
			progressBarDtc.Value = 0;
			timerDtc1.Enabled = true;
		}
		
		void clearDtc()
		{
			try
			{
				tabControl1.SelectedTab = tabControl1.TabPages[1];
				clearBarDtc();
				addTxt1("[0x14 clearDiagnosticInfo]\r\n");
				if (elm327Flag == true)
				{
					//textBoxVinCode.Text.ToCharArray
					hidesend = 0;
					
					//Enabled = false;
					dtcFlag = false;
					waitfor = 0x54;
					//textBoxDtc.Text = "";
					Write("14FF00\r");
				
					if (timeout == 0)
					{
						delayloop(250);
						
						int sidByte = rxBuf[1];
						switch (sidByte)
						{
							case 0x54:
								textBoxDtc.Text = "";
								textBoxTs.Text = "";
								listBox1.Items.Clear();
								labelFaultsDetected.Text = "";
								addTxt1("Diagnostic Codes Cleared Successfully \r\n");
								break;
							case 0x7F:
								addTxt1("Diagnostic Codes failed to clear \r\n");
								break;
							default:
								addTxt1("No Response from ECU...\r\n");
								break;
						}
						
					}
					// clears fault code definition from textBoxDtc and clears all items from listview1 - the DTC fault code display
					waitfor = 0x00;
					Enabled = true;
					return;				
				}
				if (j2534Flag == true)
				{
					try
					{
						byte[] clearDtc = new byte[] { 0, 0, ecuRxIdentifier1, ecuRxIdentifier2, serviceClearDiagnosticInformation, 0xFF, 0x00};
						string clearDtcMsg = sendPassThruMsg(clearDtc);
						// Logic for responsees needs to be done here...
						string responseData = clearDtcMsg.Replace(" ", "");
						string responseData2 = responseData.Substring(8, 2);
						int response = int.Parse(responseData2, System.Globalization.NumberStyles.HexNumber);
						switch(response)
						{
							case 0x54:
								textBoxDtc.Text = "";
								textBoxTs.Text = "";
								labelFaultsDetected.Text = "";
								listBox1.Items.Clear();
								addTxt1($"{textBoxEcu.Text} Diagnostic Codes Cleared. \r\n");
								break;
							case 0x7F:
								addTxt1($"{textBoxEcu.Text} DTC Failed to Clear... \r\n");
								break;
						}
						return;					
					}
					catch (Exception ex)
					{
						addTxt1($"{textBoxEcu.Text} DTC Failed to Clear, ECU OFFLINE: " + ex.Message);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				// Catching any exception of type Exception
				// Handle the exception here, such as logging or displaying an error message
				addTxt1($"{textBoxEcu.Text} Error Clearing DTC: " + ex.Message);
			}
		}
		//////////////////////////////////////////////////////////////////
		// 16.7 Mandatory PIDs 
		// Table 16.3 defines the mandatory PIDs that shall be supported by all ECUs connected to the diagnostic 
		// link connectosr. 
		// PID Description Classification Size 
		// $0200 Number of Continuous DTCs NUM 1 Byte 
		// $0202 Number of DTCs from most recent test NUM 1 Byte 
		// Global Diagnostic Specification – Part One R&VT/EESE - Core Systems Engineering Dept. 
		// FORD CONFIDENTIAL Page 70 of 148 4/25/03, version 2003.0 
		// $D100 ECU Operating State / Mode SED 1 Byte 
		// $E200 Software Version Number PKT 3 Bytes 
		// $E217 Part Number Identification Base PKT 4 Bytes 
		// $E219 Part Number Identification Suffix PKT 2 Bytes 
		// $E21A Part Number Identification Prefix PKT 4 Bytes 
		// Table 16.3 Mandatory Supported PIDs 
		// 16.7.1 Number of Continuous DTCs (PID $0200) 
		// The Number of Continuous DTCs PID contains the number of continuous DTCs currently being stored by 
		// the ECU. 
		// 16.7.2 Number of Trouble Codes Set Due to Diagnostic Test (PID $0202) 
		// The number of trouble codes Set due to diagnostic test PID contains the number of on-demand DTCs 
		// generated during the most recent diagnostic test executed by an ECU. 
		// 16.7.3 ECU Operating State (PID $D100) 
		// ECU Operating State PID contains the ECU’s current operating state/mode, see Table 16.4.


		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//   ██████╗ ██████╗ ███╗   ██╗████████╗██╗███╗   ██╗██╗   ██╗ ██████╗ ██╗   ██╗███████╗    ██████╗ ████████╗ ██████╗
		//  ██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝██║████╗  ██║██║   ██║██╔═══██╗██║   ██║██╔════╝    ██╔══██╗╚══██╔══╝██╔════╝
		//  ██║     ██║   ██║██╔██╗ ██║   ██║   ██║██╔██╗ ██║██║   ██║██║   ██║██║   ██║███████╗    ██║  ██║   ██║   ██║     
		//  ██║     ██║   ██║██║╚██╗██║   ██║   ██║██║╚██╗██║██║   ██║██║   ██║██║   ██║╚════██║    ██║  ██║   ██║   ██║     
		//  ╚██████╗╚██████╔╝██║ ╚████║   ██║   ██║██║ ╚████║╚██████╔╝╚██████╔╝╚██████╔╝███████║    ██████╔╝   ██║   ╚██████╗
		//   ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝  ╚═════╝ ╚══════╝    ╚═════╝    ╚═╝    ╚═════╝
		//                                                                                                                   
		///////////////////////////////////////////////////////////////////////////////////
		/// READ STANDARD AND CONTINUOUS DIAGNOSTIC CODES - SERVICE 0X18, 0X22
		/// 
		///	6.3.2.3 Reporting Continuous DTCs (Ford-9141, SCP and UBP)
		///	In response to a Request Parameter by PID (mode $22) message, with the address set to $0200 - Number
		///	of Continuous DTCs, an ECU shall return a Report Parameter by PID (mode $62) message with the
		///	number of continuous DTCs currently logged. In response to a single Request Stored Codes (mode $13)
		///	message, an ECU shall report all continuous DTCs logged by returning as many consecutive Report
		///	Stored Codes (mode $53) messages as is required. Refer to Table 6.4 for an example of the number of
		///	consecutive messages returned in relation to the number of continuous codes in a module.
		///	NOTE: There is no provision for a tester to request, or for an ECU to report, less than all of the
		///	continuous DTCs logged by a module.
		///	Continuous DTCs
		///	Logged
		///	Messages
		///	Returned
		///	Method
		///	0 1 All six Bytes of the message reserved for three DTCs shall be padded with $00
		///	1 or 2 1 DTC(s) in Data Bytes 2 & 3 and/or Data Bytes 4 & 5; the remaining Bytes shall be
		///	padded with $00
		///	3 1 All three DTCs shall be reported in Data Bytes 2 to 7 of the message
		///	4 or 5 2 The first message shall return three DTCs. The second message shall follow the
		///	method used for one or two DTCs as explained above
		///	6 2 Each message shall return three DTCs
		///	n m Follow the same method presented above for four to six DTCs
		///	Table 6.4 Consecutive Messages Returned When Reporting DTCs
		/// <summary>
		/// CONTINUOUS DTC AND STANDARD DTC READ AND CLEAR LOG
		/// </summary>

		string dtc = "";
		void readContinuousCodes()
		{
			try
			{
				tabControl1.SelectedTab = tabControl1.TabPages[1];
				readBarDtc();
				addTxt1("[0x18 readDTCByStatus] \r\n");
				if (elm327Flag == true)
				{
					dtcFlag = true;
					startDiagnosticSession(standardDiagnostic);
					waitfor = 0x58;
					addTxt1("Reading Standard Codes\r\n");
					Write("1800FF00\r");
					if (timeout == 0)
					{
						delayloop(250);
						int sidByte = rxBuf[1];
						switch (sidByte)
						{
							case 0x7F:
								addTxt1("Error Reading DTC...\r\n");
							    break;
							case 0x62:
								break;
							default:
								addTxt1("No Response from ECU ... \r\n");
								break;
						}
					}
					dtcFlag = false;
					waitfor = 0x62;
					addTxt1("Reading Continous Trouble Code Memory #1: \r\n");
					Write("22E6F3\r");
					if (timeout == 0)
					{
						delayloop(0xFA);
						int sidByte = rxBuf[1];
						waitfor = 0;
					}
					waitfor = 0x62;
					addTxt1("Reading Continous Trouble Code Memory #2: \r\n");
					Write("220200\r");
					if (timeout == 0)
					{
						delayloop(0xFA);
						int sidByte = rxBuf[1];
						waitfor = 0;
					}
					waitfor = 0x62;
					if (timeout == 0)
					{
						delayloop(0xFA);
						int sidByte = rxBuf[1];
						switch (sidByte)
						{
							case 0x7F:
								addTxt1("Error...\r\n");
							    break;
							case 0x62:
								break;
							default:
								addTxt1("No Response from ECU ... \r\n");
								break;
						}
						// rx response 04 62 02 02 00 00 00 00 if no codes from test
						waitfor = 0;
					}
					dtcFlag = false;
					Enabled = true;
					return;
				}
				if (j2534Flag == true)
				{
					try
					{
						string dtc = "";
						labelFaultsDetected.Text = "";
						startDiagnosticSession(ecuAdjustment);
						addTxt1("Reading Continous Trouble Code Memory #1: \r\n");
						byte[] readContinuousCodes01 = new byte[] { 0, 0, ecuRxIdentifier1 , ecuRxIdentifier2, serviceReadDataByCommonIdentifier, 0xE6, 0xF3};
						string readContinuousCodes01Msg = sendPassThruMsg(readContinuousCodes01);
						string responseCode01 = readContinuousCodes01Msg.Replace(" ", "");
						responseCode01 = responseCode01.Substring(0, 4);
						//  00  00  07  28  62  E6  F3  0A
						addTxt1("Reading Continous Trouble Code Memory #2: \r\n");
						byte[] readContinuousCodes02 = new byte[] { 0, 0, ecuRxIdentifier1 , ecuRxIdentifier2, serviceReadDataByCommonIdentifier, 0x02, 0x00};
						string readContinuousCodes02Msg = sendPassThruMsg(readContinuousCodes02);
						string responseCode02 = readContinuousCodes02Msg.Replace(" ", "");
						responseCode02 = responseCode02.Substring(14, 2);
						int numberOfContCodes = Convert.ToInt32(responseCode02, 16);
						addTxt1("Continuous Codes: " + numberOfContCodes + "\r\n");
						// 00  00  07  28  62  02  00  0B  
						byte[] readDtc = new byte[] { 0, 0, ecuRxIdentifier1 , ecuRxIdentifier2, serviceReadDtcByStatus, 0x00, 0xFF, 0x00};
						string readDtcMsg = sendPassThruMsg(readDtc);
						//0000 0728 58 0A A68020 5187E0 920161 C14060 C15960 C00260 C10060 C12160 C16460 C15160
						string responseData = readDtcMsg.Replace(" ", "");
						string responseData2 = responseData.Substring(8, 2);
						string numberOfDtc = responseData.Substring(10, 2);
						int numberOfDtcInt = Convert.ToInt32(numberOfDtc, 16) * 6;
						addTxt1("Codes Detected: " + (numberOfDtcInt / 6) + "\r\n");
						labelFaultsDetected.Text = (numberOfDtcInt / 6).ToString();
						labelFaultsDetected.ForeColor = System.Drawing.Color.Crimson;
						int j = 0;
						int response = int.Parse(responseData2, System.Globalization.NumberStyles.HexNumber);
						switch(response)
						{
							case 0x58:
								// only try to parse fault codes if we get a positive response
								for (int i = 12; j <= numberOfDtcInt; j++)
								{
									try
									{
										dtc += responseData.Substring(i, 6);
										string dtcStr = dtc.Substring(0, 4);
										string faultDef = dtcStr;
										int dtcInt = Convert.ToInt32(dtcStr, 16);;
										string code = VR_formatDTC(dtcInt);
										string code2 = dtcInt.ToString("X");
										code2 = code2.Substring(1, 3);
										dtc += " (" + code + code2 + ") ";
				
										string mod = textBoxEcu.Text.Substring(0, 4);
										dtc += mod;
										listBox1.Items.Add(dtc);
										dtc = "";
										faultDef = "";
										i = i + 6;
									}
									catch (Exception ex)
									{
										// Code to handle the exception
										//addTxt1("Error Parsing DTC\r\n");
										//addTxt1("An exception occurred: " + ex.Message);
									}
								}
								addTxt1($"{textBoxEcu.Text} Diagnostic Codes Read Successfully \r\n");
								break;
							case 0x7F:
								addTxt1($"{textBoxEcu.Text} Diagnostic Codes failed to read \r\n");
								break;
						}
						return;					
					}
					catch (Exception ex)
					{
						addTxt1($"{textBoxEcu.Text} Error Occured: " + ex.Message);
						return;
					}
				}
			}
			catch (Exception ex)
			{
				// Catching any exception of type Exception
				// Handle the exception here, such as logging or displaying an error message
				addTxt1($"{textBoxEcu.Text} Error Reading DTC: " + ex.Message);
			}
		}
		///////////////////////////////////////////////////////////////////////////////////////////////
		/// Convert from Hexadecimal Fault Code from CAN message to human readable format EG P0100
		/// Code pinched from VTM "Vehicle Traffic Monitor" internal ford tool                                   
		///////////////////////////////////////////////////////////////////////////////////////////
		private static string VR_formatDTC(int dtc)
		{
			if (dtc <= 65535)
			{
				int nibble = (dtc >> 12) & 0xF;
				switch (nibble)
				{
					case 0:
					case 1:
					case 2:
					case 3:
						return "P" + nibble;
					case 4:
					case 5:
					case 6:
					case 7:
						return "C" + (nibble - 4);
					case 8:
					case 9:
					case 10:
					case 11:
						return "B" + (nibble - 8);
					case 12:
					case 13:
					case 14:
					case 15:
						return "U" + (nibble - 12);
				}
				return VR_formatHexNumber(dtc, 4);
			}
			return null;
		}
 		//////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		///                                        
		///////////////////////////////////////////////////////////////////////////////////////////
		private static string VR_formatHexNumber(int num, int digits)
		{
			var str = "";
			if (digits == 0)
			{
				int tempNum = num;
				while (tempNum != 0)
				{
					tempNum >>= 8;
					digits++;
				}
			}
			for (int i = 0; i < digits; i++)
			{
				int shift = 4 * (digits - i - 1);
				int temp = (num & (15 << shift)) >> shift;
				if (temp < 10)
				{
					str += (char)(48 + temp);
				}
				else if (temp <= 16)
				{
					str += (char)(65 + temp - 10);
				}
			}
			return str;
		}

		// fault code parser via FGCOM.Orion.faultCodeDefinitions
		// if (string in FGCOM.Orion.faultCodeDefinitions)
		// {
		//	   print string to textBoxDTC
		//	}
		///////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// CLEAR DTC ALL MSCAN ECU FUNCTION
		/// </summary>
		/////////////////////////////////////////////////////////////////////////////
		private async void clearAllDTCAllModules()
		{
			// Run the loop asynchronously
            await Task.Run(() =>
            {
				try
				{
					dtcFlag = false;
					reset2Aim();
					clearDtc();
					reset2Acm();
					clearDtc();
					reset2Bem();
					clearDtc();
					reset2Bpm();
					clearDtc();
					reset2Fdim();
					clearDtc();
					reset2Ipc();
					clearDtc();
					reset2Pam();
					clearDtc();
					reset2Pcm();
					clearDtc();
					reset2Tcm();
					clearDtc();
					reset2Him();
					clearDtc();
					reset2Rcm();
					clearDtc();
					reset2Abs();
					clearDtc();
					return;				
				}
				catch (Exception ex)
				{
					addTxt1("An Error Occured: " + ex.Message);
					return;
				}
			});
		}
		///////////////////////////////////////////////////////////////////////////////////////////////
		///
		///
		///                                        
		///////////////////////////////////////////////////////////////////////////////////////////
		private async void readAllDTCForAllModules()
		{
			// Run the loop asynchronously
            await Task.Run(() =>
            {
				try
				{
					hidesend = 1;
					dtcFlag = true;
					addTxt1("Reading Standard Diagnostic Trouble Codes...\r\n");
					if (instrumentCluster = true)
					{
						dtcFlag = true;
						reset2Ipc();
						addTxt1("IPC Fault Codes:\r\n");
						readContinuousCodes();
					}
					
					if (bodyElectronicModule = true)
					{
						dtcFlag = true;
						reset2Bem();
						addTxt1("BEM Fault Codes:\r\n");
						readContinuousCodes();
					}

					if (audioControlModule = true)
					{
						dtcFlag = true;
						reset2Acm();
						addTxt1("ACM Fault Codes:\r\n");
						readContinuousCodes();
					}
					
					if (parkingAidModule = true)
					{
						dtcFlag = true;
						reset2Pam();
						addTxt1("PAM Fault Codes:");
						readContinuousCodes();
					}
					
					if (audioInterfaceModule = true)
					{
						dtcFlag = true;
						reset2Aim();
						addTxt1("AIM Fault Codes:");
						readContinuousCodes();
					}
					
					if (bluetoothPhoneModule = true)
					{
						dtcFlag = true;
						reset2Bpm();
						addTxt1("BPM Fault Codes:");
						readContinuousCodes();
					}
					
					if (frontDisplayInterfaceModule = true)
					{
						dtcFlag = true;
						reset2Fdim();
						addTxt1("FDIM Fault Codes:");
						readContinuousCodes();
					}
					reset2Pcm();
					readContinuousCodes();
					reset2Tcm();
					readContinuousCodes();
					reset2Rcm();
					readContinuousCodes();
					reset2Him();
					readContinuousCodes();
					reset2Abs();
					readContinuousCodes();


					dtcFlag = false;
					Enabled = true;
					return;
				}
				catch (Exception ex)
				{
					addTxt1("An Error Occured: " + ex.Message);
					return;
				}
			});
		}
	}
}

