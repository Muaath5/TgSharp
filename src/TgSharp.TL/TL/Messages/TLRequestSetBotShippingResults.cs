using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;

namespace TeleSharp.TL.Messages
{
    [TLObject(-436833542)]
    public class TLRequestSetBotShippingResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -436833542;
            }
        }

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public string Error { get; set; }
        public TLVector<TLShippingOption> ShippingOptions { get; set; }
        public bool Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            QueryId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Error = StringUtil.Deserialize(br);
            else
                Error = null;

            if ((Flags & 2) != 0)
                ShippingOptions = (TLVector<TLShippingOption>)ObjectUtils.DeserializeVector<TLShippingOption>(br);
            else
                ShippingOptions = null;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(QueryId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Error, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(ShippingOptions, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}