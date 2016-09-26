﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using STAR.Model;
using System;
using System.Linq;

namespace STAR.ViewModel
{
    /*
     * Used as an interface to the list of packets
     * in order to display information on the UI
     */

    class PacketView
    {
        private const string timeFormatUI = "HH:mm:ss.fff";

<<<<<<< HEAD
        public long TimeTicks { get; private set; }
        public string TimeString { get; private set; }
        public byte EntryPort { get; private set; }
        public byte ExitPort { get; private set; }
        public Type PacketType { get; private set; }
        public bool DataPacket { get; private set; }
        public string PacketTypeString { get; private set; }

        public string Message          { get; private set; }
        public string EndCode          { get; private set; }
        public bool   Valid            { get; private set; }
        public bool   CRCError         { get; private set; }
        public bool   SequenceIdError  { get; private set; }
        public bool   DuplicatePacketError { get; private set; }
        public byte[] Cargo { get; private set; }

        public ushort ProtocolId           { get; private set; }
        public byte   DestinationKey       { get; private set; }
        public uint   SequenceId           { get; private set; }
        public byte[] SourcePathAddress    { get; private set; }
        public byte   SourceLogicalAddress { get; private set; }
        public ushort TransactionId        { get; private set; }
        public byte   ExtendedWriteAddress { get; private set; }
        public uint   WriteAddress         { get; private set; }
        public uint   ReadAddress          { get; private set; }
        public uint   DataLength           { get; private set; }
        public byte   HeaderCRC            { get; private set; }
        public byte[] DataBytes            { get; private set; }
        public byte   DataCRC              { get; private set; }
        public byte   Status               { get; private set; }
        public byte   ReplyCRC             { get; private set; }
        public byte   DestinationLogicalAddress { get; private set; }

        

        public PacketView() {
=======
        public bool Valid { get; private set; }
        public bool CRCError { get; private set; }
        public bool SequenceIdError { get; private set; }
        public bool DuplicatePacketError { get; private set; }
        public bool DataPacket { get; private set; }
        public Type PacketType { get; private set; }
        public long TimeTicks { get; private set; }
>>>>>>> 04dc3e7a9fa920a1d8a787577a84d9fb305b9b01

        public string TimeString { get; private set; }
        public string EntryPort { get; private set; }
        public string ExitPort { get; private set; }
        public string PacketTypeString { get; private set; }
        public string Message { get; private set; }
        public string EndCode { get; private set; }
        public string Cargo { get; private set; }
        public string ProtocolId { get; private set; }
        public string DestinationKey { get; private set; }
        public string SequenceId { get; private set; }
        public string SourcePathAddress { get; private set; }
        public string SourceLogicalAddress { get; private set; }
        public string TransactionId { get; private set; }
        public string ExtendedWriteAddress { get; private set; }
        public string WriteAddress { get; private set; }
        public string ExtendedReadAddress { get; private set; }
        public string ReadAddress { get; private set; }
        public string DataLength { get; private set; }
        public string HeaderCRC { get; private set; }
        public string DataBytes { get; private set; }
        public string DataCRC { get; private set; }
        public string Status { get; private set; }
        public string ReplyCRC { get; private set; }
        public string DestinationPathAddress { get; private set; }
        public string DestinationLogicalAddress { get; private set; }

        // Constructor for PacketView
        // Takes a Packet of any time as a parameter
        // initialises the elements which will be displayed
        // on the GUI
        public PacketView(Packet packet)
        {
            TimeTicks = packet.TimeStamp.Ticks;
            TimeString = string.Format("{0:" + timeFormatUI + "}", packet.TimeStamp);
            EntryPort = byteToString(packet.EntryPort);
            ExitPort = byteToString(packet.ExitPort);

            PacketType = packet.GetType();

            if (PacketType == typeof(ErrorPacket))
            {
                // Error packet
                PacketTypeString = "Error";
                DataPacket = false;
                Message = (packet as ErrorPacket).Message;
                return;
            }

            // Common to all data packets
            DataPacket = true;
            ProtocolId = byteToString((packet as DataPacket).Protocol);
            EndCode = (packet as DataPacket).EndCode;
            Valid = (packet as DataPacket).Valid;
            Message = Valid ? "" : "No EOP";

            SequenceIdError = (packet as DataPacket).SequenceIdError;
            if(SequenceIdError) {
                Message = "Out of sequence";
                Valid = false;
            }

            DuplicatePacketError = (packet as DataPacket).DuplicatePacketError;
            if (DuplicatePacketError)
            {
                Message = "Duplicate";
                Valid = false;
            }

            CRCError = (packet as DataPacket).CRCError;
            if (CRCError)
            {
                Message = "CRC Error";
                Valid = false;
            }

            if (PacketType == typeof(NonRmapPacket))
            {
                NonRmapPacket pkt = packet as NonRmapPacket;
                PacketTypeString = "Non RMAP";
<<<<<<< HEAD
            }
            else if (PacketType == typeof(WriteCommandPacket))
            {
                SequenceId = pkt.SequenceNumber;
                Cargo = pkt.Cargo;
            } else if(PacketType == typeof(WriteCommandPacket)) {
                WriteCommandPacket pkt = packet as WriteCommandPacket;
                PacketTypeString = "Write command";
                DestinationKey = pkt.DestinationKey;
                SourcePathAddress = pkt.SourcePathAddress;
                SourceLogicalAddress = pkt.SourceLogicalAddress;
                TransactionId = pkt.TransactionId;
                ExtendedWriteAddress = pkt.ExtendedWriteAddress;
                WriteAddress = pkt.WriteAddress;
                DataLength = pkt.DataLength;
                HeaderCRC = pkt.HeaderCRC;
                DataBytes = pkt.DataBytes;
                DataCRC = pkt.DataCRC;
            }
            else if (PacketType == typeof(WriteResponsePacket))
            {
                WriteResponsePacket pkt = packet as WriteResponsePacket;
                PacketTypeString = "Write response";
                Status = pkt.Status;
                DestinationLogicalAddress = pkt.DestinationLogicalAddress;
                TransactionId = pkt.TransactionId;
                ReplyCRC = pkt.ReplyCRC;
            }
            else if (PacketType == typeof(ReadCommandPacket))
            {
                ReadCommandPacket pkt = packet as ReadCommandPacket;
                PacketTypeString = "Read command";
                DestinationKey = pkt.DestinationKey;
                SourcePathAddress = pkt.SourcePathAddress;
                SourceLogicalAddress = pkt.SourceLogicalAddress;
                TransactionId = pkt.TransactionId;
                ExtendedWriteAddress = pkt.ExtendedWriteAddress;
                ReadAddress = pkt.ReadAddress;
                DataLength = pkt.DataLength;
                HeaderCRC = pkt.HeaderCRC;
            }
            else
            {
                ReadResponsePacket pkt = packet as ReadResponsePacket;
                PacketTypeString = "Read response";
                Status = pkt.Status;
                DestinationLogicalAddress = pkt.DestinationLogicalAddress;
                TransactionId = pkt.TransactionId;
                DataLength = pkt.DataLength;
                HeaderCRC = pkt.HeaderCRC;
                DataBytes = pkt.DataBytes;
                DataCRC = pkt.DataCRC;
=======
                DestinationPathAddress = byteToString(pkt.PathAddress);
                DestinationLogicalAddress = byteToString(pkt.LogicalAddress);
                SequenceId = byteToString(pkt.SequenceNumber);
                Cargo = byteToString(pkt.Cargo);
            } else if(PacketType == typeof(WriteCommandPacket)) {
                WriteCommandPacket pkt = packet as WriteCommandPacket;
                PacketTypeString = "Write command";
                DestinationPathAddress = byteToString(pkt.PathAddress);
                DestinationLogicalAddress = byteToString(pkt.LogicalAddress);
                DestinationKey = byteToString(pkt.DestinationKey);
                SourcePathAddress = byteToString(pkt.SourcePathAddress);
                SourceLogicalAddress = byteToString(pkt.SourceLogicalAddress);
                TransactionId = byteToString(pkt.TransactionId);
                ExtendedWriteAddress = byteToString(pkt.ExtendedWriteAddress);
                WriteAddress = byteToString(pkt.WriteAddress);
                DataLength = byteToString(pkt.DataLength);
                HeaderCRC = byteToString(pkt.HeaderCRC);
                DataBytes = byteToString(pkt.DataBytes);
                DataCRC = byteToString(pkt.DataCRC);
            } else if(PacketType == typeof(WriteReplyPacket)) {
                WriteReplyPacket pkt = packet as WriteReplyPacket;
                PacketTypeString = "Write reply";
                SourcePathAddress = byteToString(pkt.PathAddress);
                SourceLogicalAddress = byteToString(pkt.LogicalAddress);
                Status = byteToString(pkt.Status);
                DestinationLogicalAddress = byteToString(pkt.DestinationLogicalAddress);
                TransactionId = byteToString(pkt.TransactionId);
                ReplyCRC = byteToString(pkt.ReplyCRC);
            } else if(PacketType == typeof(ReadCommandPacket)) {
                ReadCommandPacket pkt = packet as ReadCommandPacket;
                PacketTypeString = "Read command";
                DestinationPathAddress = byteToString(pkt.PathAddress);
                DestinationLogicalAddress = byteToString(pkt.LogicalAddress);
                DestinationKey = byteToString(pkt.DestinationKey);
                SourcePathAddress = byteToString(pkt.SourcePathAddress);
                SourceLogicalAddress = byteToString(pkt.SourceLogicalAddress);
                TransactionId = byteToString(pkt.TransactionId);
                ExtendedReadAddress = byteToString(pkt.ExtendedReadAddress);
                ReadAddress = byteToString(pkt.ReadAddress);
                DataLength = byteToString(pkt.DataLength);
                HeaderCRC = byteToString(pkt.HeaderCRC);
            } else {
                ReadReplyPacket pkt = packet as ReadReplyPacket;
                PacketTypeString = "Read reply";
                SourcePathAddress = byteToString(pkt.PathAddress);
                SourceLogicalAddress = byteToString(pkt.LogicalAddress);
                Status = byteToString(pkt.Status);
                DestinationLogicalAddress = byteToString(pkt.DestinationLogicalAddress);
                TransactionId = byteToString(pkt.TransactionId);
                DataLength = byteToString(pkt.DataLength);
                HeaderCRC = byteToString(pkt.HeaderCRC);
                DataBytes = byteToString(pkt.DataBytes);
                DataCRC = byteToString(pkt.DataCRC);
            }
        }
        
        // Method to convert byte array to string
        private string byteToString(byte[] bytes) {
            string tmp = bytes == null ? "" : string.Concat(bytes.Select(b => b.ToString("x2")));
            if(tmp.Length > 2) {
                tmp = tmp.TrimStart('0');
            }
            if(tmp.Length % 2 != 0) {
                tmp = "0" + tmp;
>>>>>>> 04dc3e7a9fa920a1d8a787577a84d9fb305b9b01
            }
            return tmp;
        }

        // Overload for single byte
        private string byteToString(byte b) {
            return b.ToString("x2");
        }

        // Overload for uint
        private string byteToString(uint val) {
            byte[] bytes = BitConverter.GetBytes(val);
            if(BitConverter.IsLittleEndian) {
                Array.Reverse(bytes);
            }
            return byteToString(bytes);
        }

        // Overload for ushort
        private string byteToString(ushort val) {
            byte[] bytes = BitConverter.GetBytes(val);
            if(BitConverter.IsLittleEndian) {
                Array.Reverse(bytes);
            }
            return byteToString(bytes);
        }
    }

    [TestClass]
    public class PacketViewTester {
        [TestMethod]
        public void testVariableGetters() {
            Packet testPacket = new Packet(1, 2, "09-09-2016 00:35:07.223");
            PacketView testPacketView = new PacketView(testPacket);
            bool isAccurate = false;
            try
            {
                if (testPacketView.CRCError == false && testPacketView.Valid == false && testPacketView.DataPacket == false)
                {
                    isAccurate = true;
                }
            }
            catch (Exception)
            {
                isAccurate = false;
            }

            Assert.IsFalse(isAccurate);
        }
    }
}
