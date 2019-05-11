﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoLibrary
{
    public partial class YouTubeVideo
    {
        public bool Is3D
        {
            get
            {
                switch (FormatCode)
                {
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                    case 100:
                    case 101:
                    case 102:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public bool IsAdaptive =>
            this.AdaptiveKind != AdaptiveKind.None;

        public AdaptiveKind AdaptiveKind
        {
            get
            {
                switch (FormatCode)
                {
                    case 133:
                    case 134:
                    case 135:
                    case 136:
                    case 137:
                    case 138:
                    case 160:
                    case 242:
                    case 243:
                    case 244:
                    case 247:
                    case 248:
                    case 264:
                    case 271:
                    case 272:
                    case 278:
                    case 313:
                        return AdaptiveKind.Video;
                    case 139:
                    case 140:
                    case 141:
                    case 171:
                    case 172:
                    case 249:
                    case 250:
                    case 251:
                        return AdaptiveKind.Audio;
                    default:
                        return AdaptiveKind.None;
                }
            }
        }

        public int AudioBitrate
        {
            get
            {
                switch (FormatCode)
                {
                    case 5:
                    case 6:
                    case 250:
                        return 64;
                    case 17:
                        return 24;
                    case 18:
                    case 82:
                    case 83:
                        return 96;
                    case 22:
                    case 37:
                    case 38:
                    case 45:
                    case 46:
                    case 101:
                    case 102:
                    case 172:
                        return 192;
                    case 34:
                    case 35:
                    case 43:
                    case 44:
                    case 100:
                    case 140:
                    case 171:
                        return 128;
                    case 36:
                        return 38;
                    case 84:
                    case 85:
                        return 152;
                    case 251:
                        return 160;
                    case 139:
                    case 249:
                        return 48;
                    case 141:
                        return 256;
                    default:
                        return -1;
                }
            }
        }

        public int Resolution
        {
            get
            {
                switch (FormatCode)
                {
                    case 5:
                    case 36:
                    case 83:
                    case 133:
                    case 242:
                        return 240;
                    case 6:
                        return 270;
                    case 17:
                    case 160:
                    case 278:
                        return 144;
                    case 18:
                    case 34:
                    case 43:
                    case 82:
                    case 100:
                    case 101:
                    case 134:
                    case 243:
                        return 360;
                    case 22:
                    case 45:
                    case 84:
                    case 102:
                    case 136:
                    case 247:
                        return 720;
                    case 35:
                    case 44:
                    case 135:
                    case 244:
                        return 480;
                    case 37:
                    case 46:
                    case 137:
                    case 248:
                        return 1080;
                    case 38:
                        return 3072; // what
                    case 85:
                        return 520;
                    case 138:
                    case 272:
                    case 313:
                        return 2160;
                    case 264:
                    case 271:
                        return 1440;
                    default:
                        return -1;
                }
            }
        }

        public override VideoFormat Format
        {
            get
            {
                switch (FormatCode)
                {
                    case 5:
                    case 6:
                    case 34:
                    case 35:
                        return VideoFormat.Flash;
                    case 13:
                    case 17:
                    case 36:
                        return VideoFormat.Mobile;
                    case 18:
                    case 22:
                    case 37:
                    case 38:
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                    case 133:
                    case 134:
                    case 135:
                    case 136:
                    case 137:
                    case 138:
                    case 160:
                    case 264:
                    case 139:
                    case 140:
                    case 141:
                        return VideoFormat.Mp4;
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 100:
                    case 101:
                    case 102:
                    case 242:
                    case 243:
                    case 244:
                    case 247:
                    case 248:
                    case 271:
                    case 272:
                    case 278:
                    case 171:
                    case 172:
                    case 249:
                    case 250:
                    case 251:
                    case 313:
                        return VideoFormat.WebM;
                    default:
                        return VideoFormat.Unknown;
                }
            }
        }

        public AudioFormat AudioFormat
        {
            get
            {
                switch (FormatCode)
                {
                    case 5:
                    case 6:
                        return AudioFormat.Mp3;
                    case 13:
                    case 17:
                    case 18:
                    case 22:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                    case 139:
                    case 140:
                    case 141:
                        return AudioFormat.Aac;
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 100:
                    case 101:
                    case 102:
                    case 171:
                    case 172:
                        return AudioFormat.Vorbis;
                    case 249:
                    case 250:
                    case 251:
                        return AudioFormat.Opus;
                    default:
                        return AudioFormat.Unknown;
                }
            }
        }
    }
}