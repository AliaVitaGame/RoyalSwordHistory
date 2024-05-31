
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // ------------------------------------

        //--Main--
        public string NamePlayer;
        public int CoinCount;

        //--Flags--
        public int SelectedFlagID;
        public bool[] IsBuyFlag = new bool[113];

        //--Settings--
        public float MusicVolume;
        public float AudioEffectsVolume;

        // ------------------------------------


        public SavesYG()
        {

        }
    }
}
