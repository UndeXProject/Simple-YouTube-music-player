using System;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using Un4seen.Bass.AddOn.Tags;

namespace playerControl
{
    public class playerControl
    {
        private static int _stream = 0;
        private static bool mute = false;
        public static bool pause = false;
        public static int mode = 1;
        public static void Init(int stream)
        {
            _stream = stream;
            BassNet.Registration("undex.project@gmail.com", "2X8141823152222");
            Bass.BASS_ChannelSlideAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, 0.5f, 0);
        }

        public static TAG_INFO GetMP3Tags()
        {
            TAG_INFO tagInfo = new TAG_INFO();
            var tag = Bass.BASS_ChannelGetTags(_stream, BASSTag.BASS_TAG_META);
            if (tag != null)
            {
                BassTags.BASS_TAG_GetFromURL(_stream, tagInfo);
            }
            return tagInfo;
        }

        public static void SetVolume(int percent)
        {
            float Volume = (float)percent / 100;
            Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, Volume);
        }

        public static bool MuteUnMute(int percent)
        {
            float Volume = (float)percent / 100;
            if (mute==false)
            {
                Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, 0.0f);
                mute = !mute;
            }
            else
            {
                Bass.BASS_ChannelSetAttribute(_stream, BASSAttribute.BASS_ATTRIB_VOL, Volume);
                mute = !mute;
            }
            return mute;
        }

        public static bool PlayPause(int stream)
        {
            if (pause == false)
            {
                pause = !pause;
                Bass.BASS_ChannelPlay(stream,false);
            }
            else
            {
                pause = !pause;
                Bass.BASS_ChannelPause(stream);
            }
            return pause;
        }

        public static void Stop(int stream)
        {
            Bass.BASS_ChannelStop(stream);
        }

        public static void PlayerSetControl(bool set)
        {
            pause = set;
        }
    }
}
