using NAudio.Wave;
using System.Diagnostics;

namespace CommanderLogic
{
    public sealed class SoundControl : IDisposable
    {
        private readonly object _musicLock = new();
        private WaveOutEvent _musicPlayer;
        private volatile bool _isDisposed;
        private float _volume = 0.7f;

        public float Volume
        {
            get => _volume;
            set => _volume = Math.Clamp(value, 0f, 1f);
        }

        public void PlayMusic(string path, bool loop = false)
        {
            if (_isDisposed) return;

            lock (_musicLock)
            {
                try
                {
                    Debug.WriteLine($"[SoundControl] Attempting to play: {path}");

                    if (!System.IO.File.Exists(path))
                    {
                        Debug.WriteLine($"[SoundControl] ERROR: File not found at path: {path}");
                        return;
                    }

                    // Stop and clean up previous playback
                    _musicPlayer?.Stop();
                    _musicPlayer?.Dispose();
                    _musicPlayer = null;

                    var reader = new AudioFileReader(path);
                    var waveOut = new WaveOutEvent { Volume = _volume };

                    // Retain both for lifecycle control
                    _musicPlayer = waveOut;

                    waveOut.PlaybackStopped += (s, e) =>
                    {
                        Debug.WriteLine("[SoundControl] Playback stopped.");
                        if (loop && !_isDisposed)
                        {
                            Debug.WriteLine("[SoundControl] Looping...");
                            reader.Position = 0;
                            waveOut.Play();
                        }
                        else
                        {
                            Debug.WriteLine("[SoundControl] Disposing reader/player.");
                            reader.Dispose();
                            waveOut.Dispose();
                        }
                    };

                    waveOut.Init(reader);
                    waveOut.Play();

                    Debug.WriteLine($"[SoundControl] Playing file: {path} successfully.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[SoundControl] Music error: {ex.Message}");
                }
            }
        }

        public void StopMusic()
        {
            lock (_musicLock)
            {
                _musicPlayer?.Stop();
            }
        }

        public void Dispose()
        {
            _isDisposed = true;
            StopMusic();
        }
    }
}