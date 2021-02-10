using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(112424539)]
    public class TLInputMediaPoll : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 112424539;
            }
        }

        public int Flags { get; set; }
        public TLPoll Poll { get; set; }
        public TLVector<byte> CorrectAnswers { get; set; }
        public string Solution { get; set; }
        public TLVector<TLMessageEntity> SolutionEntities { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Poll = (TLPoll)ObjectUtils.DeserializeObject(br);
            if (Flags & 1 != 0)
                CorrectAnswers = (TLVector<byte>)ObjectUtils.DeserializeVector<byte>(br);
            else
                CorrectAnswers = null;
            if (Flags & 2 != 0)
            {
                Solution = StringUtil.Deserialize(br);
                SolutionEntities = (TLVector<TLMessageEntity>)ObjectUtils.DeserializeVector<TLMessageEntity>(br);
            }
            else
            {
                Solution = null;
                SolutionEntities = null;
            }
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Poll, bw);
            if (Flags & 1 != 0)
                ObjectUtils.SerializeObject(CorrectAnswers, bw);
            if (Flags & 2 != 0)
            {
                StringUtil.Serialize(Solution, bw);
                ObjectUtils.SerializeObject(SolutionEntities, bw);
            }
        }
    }
}
