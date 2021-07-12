namespace EnglishKids.BuildRobots
{
    public sealed class ConveyorController : Singleton<ConveyorController>
    {
        private int _currentDetails;
        private int _allDetalls;
        private const int _maxDetails = 4;
        public static event System.Action OnRunConveyor;

        public void StartConveyorAnimation()
        {
            OnRunConveyor?.Invoke();
        }

        public void SetCountDetails(int count)
        {
            _allDetalls = count;
            _currentDetails = _maxDetails;
        }

        public void UpdateCountDetails()
        {

            if (_currentDetails > 1)
            {
                _currentDetails -= 1;
            }
            else if (_allDetalls != 0)
            {
                _currentDetails -= 1;
                _currentDetails = _allDetalls < _maxDetails ? _allDetalls : _maxDetails;
                OnRunConveyor?.Invoke();
            }
            _allDetalls -= 1;
        }
    }
}