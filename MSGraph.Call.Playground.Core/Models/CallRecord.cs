using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSGraph.Call.Playground.Core.Models
{
    public enum CallRecordType
    {
        PeerToPeer,
        Unknown,
        GroupCall,
        UnknownFutureValue
    }

    public enum Modality
    {
        Unknown,
        Audio,
        Video,
        VideoBasedScreenSharing,
        Data,
        ScreenSharing,
        UnknownFutureValue
    }

    public partial class CallRecord
    {
        [JsonProperty("endDateTime")]
        public DateTimeOffset EndDateTime { get; set; }

        [JsonProperty("lastModifiedDateTime")]
        public DateTimeOffset LastModifiedDateTime { get; set; }

        [JsonProperty("modalities")]
        public string[] Modalities { get; set; }

        [JsonProperty("organizer")]
        public Organizer Organizer { get; set; }

        [JsonProperty("participants")]
        public Organizer[] Participants { get; set; }

        [JsonProperty("startDateTime")]
        public DateTimeOffset StartDateTime { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("sessions")]
        public Session[] Sessions { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("sessions@odata.context")]
        public Uri SessionsOdataContext { get; set; }
    }

    public partial class Organizer
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("acsUser")]
        public object AcsUser { get; set; }

        [JsonProperty("spoolUser")]
        public object SpoolUser { get; set; }

        [JsonProperty("phone")]
        public object Phone { get; set; }

        [JsonProperty("guest")]
        public object Guest { get; set; }

        [JsonProperty("encrypted")]
        public object Encrypted { get; set; }

        [JsonProperty("onPremises")]
        public object OnPremises { get; set; }

        [JsonProperty("acsApplicationInstance")]
        public object AcsApplicationInstance { get; set; }

        [JsonProperty("spoolApplicationInstance")]
        public object SpoolApplicationInstance { get; set; }

        [JsonProperty("applicationInstance")]
        public object ApplicationInstance { get; set; }
    }

    public partial class User
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("tenantId")]
        public Guid TenantId { get; set; }

        [JsonProperty("registrantId")]
        public object RegistrantId { get; set; }
    }

    public partial class Session
    {
        [JsonProperty("callee")]
        public Calle Callee { get; set; }

        [JsonProperty("caller")]
        public Calle Caller { get; set; }

        [JsonProperty("endDateTime")]
        public DateTimeOffset EndDateTime { get; set; }

        [JsonProperty("modalities")]
        public string[] Modalities { get; set; }

        [JsonProperty("startDateTime")]
        public DateTimeOffset StartDateTime { get; set; }

        [JsonProperty("segments")]
        public Segment[] Segments { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("segments@odata.context")]
        public Uri SegmentsOdataContext { get; set; }
    }

    public partial class Calle
    {
        [JsonProperty("identity")]
        public Organizer Identity { get; set; }

        [JsonProperty("userAgent")]
        public UserAgent UserAgent { get; set; }

        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

    public partial class UserAgent
    {
        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("productFamily")]
        public string ProductFamily { get; set; }

        [JsonProperty("headerValue")]
        public string HeaderValue { get; set; }

        [JsonProperty("@odata.type")]
        public string OdataType { get; set; }
    }

    public partial class Segment
    {
        [JsonProperty("callee")]
        public Calle Callee { get; set; }

        [JsonProperty("caller")]
        public Calle Caller { get; set; }

        [JsonProperty("endDateTime")]
        public DateTimeOffset EndDateTime { get; set; }

        [JsonProperty("media")]
        public Media[] Media { get; set; }

        [JsonProperty("startDateTime")]
        public DateTimeOffset StartDateTime { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
    }

    public partial class Media
    {
        [JsonProperty("calleeDevice")]
        public CalleDevice CalleeDevice { get; set; }

        [JsonProperty("calleeNetwork")]
        public CalleNetwork CalleeNetwork { get; set; }

        [JsonProperty("callerDevice")]
        public CalleDevice CallerDevice { get; set; }

        [JsonProperty("callerNetwork")]
        public CalleNetwork CallerNetwork { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("streams")]
        public Stream[] Streams { get; set; }
    }

    public partial class CalleDevice
    {
        [JsonProperty("captureDeviceDriver")]
        public string CaptureDeviceDriver { get; set; }

        [JsonProperty("captureDeviceName")]
        public string CaptureDeviceName { get; set; }

        [JsonProperty("captureNotFunctioningEventRatio")]
        public long CaptureNotFunctioningEventRatio { get; set; }

        [JsonProperty("cpuInsufficentEventRatio")]
        public long CpuInsufficentEventRatio { get; set; }

        [JsonProperty("deviceClippingEventRatio")]
        public long DeviceClippingEventRatio { get; set; }

        [JsonProperty("deviceGlitchEventRatio")]
        public long DeviceGlitchEventRatio { get; set; }

        [JsonProperty("howlingEventCount")]
        public long HowlingEventCount { get; set; }

        [JsonProperty("lowSpeechLevelEventRatio")]
        public long LowSpeechLevelEventRatio { get; set; }

        [JsonProperty("lowSpeechToNoiseEventRatio")]
        public long LowSpeechToNoiseEventRatio { get; set; }

        [JsonProperty("receivedNoiseLevel")]
        public long ReceivedNoiseLevel { get; set; }

        [JsonProperty("receivedSignalLevel")]
        public long ReceivedSignalLevel { get; set; }

        [JsonProperty("renderDeviceDriver")]
        public string RenderDeviceDriver { get; set; }

        [JsonProperty("renderDeviceName")]
        public string RenderDeviceName { get; set; }

        [JsonProperty("renderMuteEventRatio")]
        public double RenderMuteEventRatio { get; set; }

        [JsonProperty("renderNotFunctioningEventRatio")]
        public long RenderNotFunctioningEventRatio { get; set; }

        [JsonProperty("renderZeroVolumeEventRatio")]
        public long RenderZeroVolumeEventRatio { get; set; }
    }

    public partial class CalleNetwork
    {
        [JsonProperty("basicServiceSetIdentifier", NullValueHandling = NullValueHandling.Ignore)]
        public string BasicServiceSetIdentifier { get; set; }

        [JsonProperty("connectionType")]
        public string ConnectionType { get; set; }

        [JsonProperty("delayEventRatio")]
        public long DelayEventRatio { get; set; }

        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        [JsonProperty("linkSpeed")]
        public long LinkSpeed { get; set; }

        [JsonProperty("macAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("port")]
        public long Port { get; set; }

        [JsonProperty("receivedQualityEventRatio")]
        public long ReceivedQualityEventRatio { get; set; }

        [JsonProperty("reflexiveIPAddress")]
        public string ReflexiveIpAddress { get; set; }

        [JsonProperty("relayIPAddress")]
        public string RelayIpAddress { get; set; }

        [JsonProperty("relayPort")]
        public long RelayPort { get; set; }

        [JsonProperty("sentQualityEventRatio")]
        public long SentQualityEventRatio { get; set; }

        [JsonProperty("subnet")]
        public string Subnet { get; set; }

        [JsonProperty("wifiBand")]
        public string WifiBand { get; set; }

        [JsonProperty("wifiChannel")]
        public long WifiChannel { get; set; }

        [JsonProperty("wifiMicrosoftDriver")]
        public string WifiMicrosoftDriver { get; set; }

        [JsonProperty("wifiMicrosoftDriverVersion")]
        public string WifiMicrosoftDriverVersion { get; set; }

        [JsonProperty("wifiRadioType")]
        public string WifiRadioType { get; set; }

        [JsonProperty("wifiVendorDriver")]
        public string WifiVendorDriver { get; set; }

        [JsonProperty("wifiVendorDriverVersion")]
        public string WifiVendorDriverVersion { get; set; }

        [JsonProperty("dnsSuffix", NullValueHandling = NullValueHandling.Ignore)]
        public string DnsSuffix { get; set; }

        [JsonProperty("wifiSignalStrength", NullValueHandling = NullValueHandling.Ignore)]
        public long? WifiSignalStrength { get; set; }
    }

    public partial class Stream
    {
        [JsonProperty("averageAudioNetworkJitter")]
        public string AverageAudioNetworkJitter { get; set; }

        [JsonProperty("averageBandwidthEstimate")]
        public long AverageBandwidthEstimate { get; set; }

        [JsonProperty("averageJitter")]
        public string AverageJitter { get; set; }

        [JsonProperty("averagePacketLossRate")]
        public double AveragePacketLossRate { get; set; }

        [JsonProperty("averageRatioOfConcealedSamples")]
        public double AverageRatioOfConcealedSamples { get; set; }

        [JsonProperty("averageRoundTripTime")]
        public string AverageRoundTripTime { get; set; }

        [JsonProperty("maxAudioNetworkJitter")]
        public string MaxAudioNetworkJitter { get; set; }

        [JsonProperty("maxJitter")]
        public string MaxJitter { get; set; }

        [JsonProperty("maxPacketLossRate")]
        public double MaxPacketLossRate { get; set; }

        [JsonProperty("maxRatioOfConcealedSamples")]
        public double MaxRatioOfConcealedSamples { get; set; }

        [JsonProperty("maxRoundTripTime")]
        public string MaxRoundTripTime { get; set; }

        [JsonProperty("packetUtilization")]
        public long PacketUtilization { get; set; }

        [JsonProperty("streamDirection")]
        public string StreamDirection { get; set; }

        [JsonProperty("streamId")]
        public string StreamId { get; set; }
    }
}
