namespace EnglishKids.BuildRobots
{
    public sealed class ConveyorController : Singleton<ConveyorController>
    {
        private int _currentCountDetails;
        private int _allCountDetalls;
        public static event System.Action RunConveyor;

        public void StartConveyorAnimation()
        {
            RunConveyor?.Invoke();
        }

        public void SetCountDetails(int count)
        {
            _allCountDetalls = count;
            _currentCountDetails = 4;
        }

        public void UpdateCountDetails()
        {

            if (_currentCountDetails > 1)
            {
                _currentCountDetails -= 1;

            }
            else if (_allCountDetalls != 0)
            {
                _currentCountDetails -= 1;
                _currentCountDetails = _allCountDetalls < 4 ? _allCountDetalls : 4;
                RunConveyor?.Invoke();
            }
            _allCountDetalls -= 1;
        }
    }
}