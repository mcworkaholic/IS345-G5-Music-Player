using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams.Effects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Music_Player
{
    public class MusicPlayer : Component
    {
        private ISoundOut _soundOut;
        private IWaveSource _waveSource;
        private Equalizer _equalizer;

        public event EventHandler<PlaybackStoppedEventArgs> PlaybackStopped;
        public IEnumerable<MMDevice> EnumerateWasapiDevices()
        {
            using (MMDeviceEnumerator enumerator = new MMDeviceEnumerator())
            {
                return enumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active);
            }
        }
        public static MMDevice GetDefaultSoundDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            }
        }
        public static MMDevice GetSoundDevice(string id)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDevice(id);
            }
        }

        public PlaybackState PlaybackState
        {
            get
            {
                if (_soundOut != null)
                    return _soundOut.PlaybackState;
                return PlaybackState.Stopped;
            }
        }
        public ISoundOut getSoundOut()
        {
            return _soundOut;
        }
        public TimeSpan Position
        {
            get
            {
                if (_waveSource != null)
                    return _waveSource.GetPosition();
                return TimeSpan.Zero;
            }
            set
            {
                if (_waveSource != null)
                    _waveSource.SetPosition(value);
            }
        }

        public TimeSpan Length
        {
            get
            {
                if (_waveSource != null)
                    return _waveSource.GetLength();
                return TimeSpan.Zero;
            }
        }
        public int Volume
        {
            get
            {
                if (_soundOut != null)
                    return Math.Min(100, Math.Max((int)(_soundOut.Volume * 100), 0));
                return 100;
            }
            set
            {
                if (_soundOut != null)
                {
                    _soundOut.Volume = Math.Min(1.0f, Math.Max(value / 100f, 0f));
                }
            }
        }

        public void Open(string filename, MMDevice device)
        {
            CleanupPlayback();
            try
            {
                _waveSource =
                    CodecFactory.Instance.GetCodec(filename)
                        .ToSampleSource()
                        .ToStereo()
                        .ChangeSampleRate(48000)
                        .AppendSource(Equalizer.Create10BandEqualizer, out _equalizer)
                        .ToWaveSource();
                _soundOut = new WasapiOut() { Latency = 100, Device = device };
                _soundOut.Initialize(_waveSource);
                if (PlaybackStopped != null) _soundOut.Stopped += PlaybackStopped;
            }
            catch (CoreAudioAPIException ex)
            {
                MessageBox.Show("Error opening file: " + ex.Message);
            }
        }
        public void Play()
        {
            if (_soundOut != null)
            {
                _soundOut.Play();
            }
        }

        public void Pause()
        {
            if (_soundOut != null)
                _soundOut.Pause();
        }
        public int Shuffle(ListBox songsListBox)
        {
            // shuffle functionality generates a random number within an array of indexes from the listbox
            // and changes the currently selected index
            var random = new Random();
            int[] indexes = new int[songsListBox.Items.Count];
            for (var i = 0; i < indexes.Length; i += 1)
                indexes[i] = i;
            List<int> list = indexes.ToList();
            int newIndex = random.Next(list.Count);
            return newIndex;
        }

        public Equalizer GetEqualizer()
        {
            return _equalizer;
        }

        private void CleanupPlayback()
        {
            if (_soundOut != null)
            {
                _soundOut.Dispose();
                _soundOut = null;
            }
            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            this.Pause();
            CleanupPlayback();
            base.Dispose(disposing);
            _equalizer?.Dispose();
        }
    }
}
