[System.Serializable]
public class SaveableProgress : ISaveable
{
    private ProgressManager progressManager;

    public SaveableProgress(ProgressManager manager)
    {
        progressManager = manager;
    }

    public void PopulateSaveData(SaveData a_SaveData)
    {
        a_SaveData.completedFases = progressManager.completedFases;
    }

    public void LoadFromSaveData(SaveData a_SaveData)
    {
        if (a_SaveData.completedFases != null && a_SaveData.completedFases.Length == progressManager.completedFases.Length)
        {
            progressManager.completedFases = a_SaveData.completedFases;
        }
    }
}