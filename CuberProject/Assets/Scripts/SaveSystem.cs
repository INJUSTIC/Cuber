using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using static System.IO.Path;
using System.Data.SqlTypes;

public static class SaveSystem
{
    private static void Save<T>(string path, T data)
    {
        FileStream filestream;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        try
        {
            filestream = new FileStream(path, FileMode.Create);
            binaryFormatter.Serialize(filestream, data);
            filestream.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }
    private static T Load<T>(string path)
    {
        //Debug.Log(path);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream filestream = new FileStream(path, FileMode.Open);
        T data = (T)binaryFormatter.Deserialize(filestream);
        filestream.Close();
        return data;
    }
    public static void SaveScore(List<string> scores)
    {
        if (scores == null)
        {
            scores = new List<string>();
            return;
        }
        string path = Application.persistentDataPath + "/Score.cub";
        Save(path, scores);
    }

    public static List<string> LoadScore()
    {
        string path = Application.persistentDataPath + "/Score.cub";
        try
        {
            if (File.Exists(path))
            {
                List<string> list = Load<List<string>>(path);
                list.Sort(new StringComparerAsInt());
                return list;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    public static void SaveVolume(float volume)
    {
        string path = Application.persistentDataPath + "/Volume.cub";
        Save(path, volume);
    }
    public static float LoadVolume()
    {
        string path = Application.persistentDataPath + "/Volume.cub";
        try
        {

            if (File.Exists(path))
            {
                return Load<float>(path);
            }
            else
            {
                return 1f;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return 0;
        }
    }
    public static void SaveMusicToggle(bool Toggle)
    {
        string path = Application.persistentDataPath + "/MusicToggle.cub";
        Save(path, Toggle);
    }
    public static bool LoadMusicToggle()
    {
        string path = Application.persistentDataPath + "/MusicToggle.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<bool>(path);
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return false;
        }
    }
    public static void SaveEffectsToggle(bool Toggle)
    {
        string path = Application.persistentDataPath + "/EffectsToggle.cub";
        Save(path, Toggle);
    }
    public static bool LoadEffectsToggle()
    {
        string path = Application.persistentDataPath + "/EffectsToggle.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<bool>(path);
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return false;
        }
    }
    public static void SaveCoins(float coins)
    {
        float AvailableCoins = LoadCoins();
        string path = Application.persistentDataPath + "/Coins.cub";
        Save(path, coins + AvailableCoins);
    }
    public static float LoadCoins()
    {
        string path = Application.persistentDataPath + "/Coins.cub";
        try
        {
            if (File.Exists(path))
            {
                float coins = Load<float>(path);
                return coins;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return 0;
        }
    }
    public static void SaveColorofCube(Color32 CubeColor)
    {
        string path = Application.persistentDataPath + "/CubeColor.cub";
        ColorData colorData = new ColorData(CubeColor);
        Save(path, colorData);
    }
    public static ColorData LoadColorofCube()
    {
        string path = Application.persistentDataPath + "/CubeColor.cub";
        try
        {
            if (File.Exists(path))
            {
                ColorData colorData = Load<ColorData>(path);
                return colorData;
            }
            else
            {
                return new ColorData(new Color32(178, 12, 0, 255));
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    public static void SaveEmissionColorofCube(Color32 CubeEmissionColor)
    {
        string path = Application.persistentDataPath + "/CubeEmissionColor.cub";
        ColorData colorData = new ColorData(CubeEmissionColor);
        Save(path, colorData);
    }
    public static ColorData LoadEmissionColorofCube()
    {
        string path = Application.persistentDataPath + "/CubeEmissionColor.cub";
        try
        {
            if (File.Exists(path))
            {
                ColorData colorData = Load<ColorData>(path);
                return colorData;
            }
            else
            {
                return new ColorData(new Color32(92, 0, 0, 255));
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    public static void SaveIndexofCube(int index)
    {
        string path = Application.persistentDataPath + "/CubeIndex.cub";
        Save(path, index);
    }
    public static int LoadIndexofCube()
    {
        string path = Application.persistentDataPath + "/CubeIndex.cub";
        try
        {
            if (File.Exists(path))
            {
                int Index = Load<int>(path);
                return Index;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return 0;
        }
    }
    public static void SaveIndexofBG(int index)
    {
        string path = Application.persistentDataPath + "/BGindex.cub";
        Save(path, index);
    }
    public static int LoadIndexofBG()
    {
        string path = Application.persistentDataPath + "/BGindex.cub";
        try
        {
            if (File.Exists(path))
            {
                int Index = Load<int>(path);
                return Index;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return 0;
        }
    }
    public static void SaveColorofBG(Color CubeColor)
    {
        string path = Application.persistentDataPath + "/BGColor.cub";
        ColorData colorData = new ColorData(CubeColor);
        Save(path, colorData);
    }
    public static ColorData LoadColorofBG()
    {
        string path = Application.persistentDataPath + "/BGColor.cub";
        try
        {
            if (File.Exists(path))
            {
                ColorData colorData = Load<ColorData>(path);
                return colorData;
            }
            else
            {
                return new ColorData(new Color(0.1424439f, 0.2670043f, 0.9150943f));
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    public static void SaveIndexofLand(int index)
    {
        string path = Application.persistentDataPath + "/Landindex.cub";
        Save(path, index);
    }
    public static int LoadIndexofLand()
    {
        string path = Application.persistentDataPath + "/Landindex.cub";
        try
        {
            if (File.Exists(path))
            {
                int Index = Load<int>(path);
                return Index;
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return 0;
        }
    }
    public static void SaveColorofLand(Color32 CubeColor)
    {
        string path = Application.persistentDataPath + "/LandColor.cub";
        ColorData colorData = new ColorData(CubeColor);
        Save(path, colorData);
    }
    public static ColorData LoadColorofLand()
    {
        string path = Application.persistentDataPath + "/LandColor.cub";
        try
        {
            if (File.Exists(path))
            {
                ColorData colorData = Load<ColorData>(path);
                return colorData;
            }
            else
            {
                return new ColorData(Color.black);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return null;
        }
    }
    public static void SaveCubeIsBought(bool IsBought, int index, int count)
    {
        List<bool> list = LoadCubeIsBought(count);
        list[index] = IsBought;
        string path = Application.persistentDataPath + "/CubeIsBought.cub";
        Save(path, list);
    }
    public static List<bool> LoadCubeIsBought(int Count)
    {
        string path = Application.persistentDataPath + "/CubeIsBought.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<List<bool>>(path);
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < Count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < Count; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveBGIsBought(bool IsBought, int index, int count)
    {
        List<bool> list = LoadBGIsBought(count);
        list[index] = IsBought;
        string path = Application.persistentDataPath + "/BGIsBought.cub";
        Save(path, list);
    }
    public static List<bool> LoadBGIsBought(int Count)
    {
        string path = Application.persistentDataPath + "/BGIsBought.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<List<bool>>(path);
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < Count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < Count; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveLandIsBought(bool IsBought, int index, int count)
    {
        List<bool> list = LoadLandIsBought(count);
        list[index] = IsBought;
        string path = Application.persistentDataPath + "/LandIsBought.cub";
        Save(path, list);
    }
    public static List<bool> LoadLandIsBought(int Count)
    {
        string path = Application.persistentDataPath + "/LandIsBought.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<List<bool>>(path);
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < Count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < Count; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveLevelIsOpened(bool IsOpened, int index, int count)
    {
        List<bool> list = LoadLevelIsOpened(count);
        list[index] = IsOpened;
        string path = Application.persistentDataPath + "/LevelIsOpened.cub";
        Save(path, list);
    }
    public static List<bool> LoadLevelIsOpened(int count)
    {
        string path = Application.persistentDataPath + "/LevelIsOpened.cub";
        try
        {
            if (File.Exists(path))
            {
                List<bool> list = Load<List<bool>>(path);
                for (int i = list.Count; i < count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < count; ++i)
                {
                    if (i == 0)
                    {
                        list.Add(true);
                        continue;
                    }
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < count; ++i)
            {
                if (i == 0)
                {
                    list.Add(true);
                    continue;
                }
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveAchievements(bool IsCompleted, int index)
    {
        List<bool> list = LoadAchievements();
        list[index] = IsCompleted;
        string path = Application.persistentDataPath + "/Achievements.cub";
        Save(path, list);
    }
    public static List<bool> LoadAchievements()
    {
        string path = Application.persistentDataPath + "/Achievements.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<List<bool>>(path);
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < 10; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < 10; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveAchievementsHasGot(bool IsCompleted, int index)
    {
        List<bool> list = LoadAchievementsHasGot();
        list[index] = IsCompleted;
        string path = Application.persistentDataPath + "/AchievementsHasGot.cub";
        Save(path, list);
    }
    public static List<bool> LoadAchievementsHasGot()
    {
        string path = Application.persistentDataPath + "/AchievementsHasGot.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<List<bool>>(path);
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < 10; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < 10; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveLevelIsCompleted(bool IsComleted, int index, int count)
    {
        List<bool> list = LoadLevelIsCompleted(count);
        list[index] = IsComleted;
        string path = Application.persistentDataPath + "/LevelIsCompleted.cub";
        Save(path, list);
    }
    public static List<bool> LoadLevelIsCompleted(int count)
    {
        string path = Application.persistentDataPath + "/LevelIsCompleted.cub";
        try
        {
            if (File.Exists(path))
            {
                List<bool> list = Load<List<bool>>(path);
                for (int i = list.Count; i < count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
            else
            {
                List<bool> list = new List<bool>();
                for (int i = 0; i < count; ++i)
                {
                    list.Add(false);
                }
                return list;
            }
        }
        catch
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < count; ++i)
            {
                list.Add(false);
            }
            return list;
        }
    }
    public static void SaveIndexofSkinCube(int index)
    {
        string path = Application.persistentDataPath + "/IndexofSkinCube.cub";
        Save(path, index);
    }
    public static int LoadIndexofSkinCube()
    {
        string path = Application.persistentDataPath + "/IndexofSkinCube.cub";
        try
        {
            if (File.Exists(path))
            {
                return Load<int>(path);
            }
            else
            {
                return 0;
            }
        }
        catch
        {
            return 0;
        }
    }
    public static void SaveTimeAfterWatchingAd(DateTime datetime)
    {
        string path = Application.persistentDataPath + "/TimeAfterWatchingAd.cub";
        List<int> list = new List<int>
        {
            datetime.Year,
            datetime.Month,
            datetime.Day,
            datetime.Hour,
            datetime.Minute,
            datetime.Second
        };
        Save(path, list);
    }
    public static DateTime? LoadDateAfterWatchingAd()
    {
        string path = Application.persistentDataPath + "/TimeAfterWatchingAd.cub";
        try
        {
            if (File.Exists(path))
            {
                List<int> list = Load<List<int>>(path);
                DateTime dateTime = new DateTime(list[0], list[1], list[2], list[3], list[4], list[5]);
                return dateTime;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    public static void SaveTimeIsFirstEntered(DateTime time)
    {
        string path = Application.persistentDataPath + "/TimeIsFirstEntered.cub";
        List<int> list = new List<int>
        {
            time.Year,
            time.Month,
            time.Day,
            time.Hour,
            time.Minute,
            time.Second
        };
        Save(path, list);
    }
    public static DateTime LoadTimeIsFirstEntered()
    {
        string path = Application.persistentDataPath + "/TimeIsFirstEntered.cub";
        try
        {
            List<int> list = Load<List<int>>(path);
            DateTime dateTime = new DateTime(list[0], list[1], list[2], list[3], list[4], list[5]);
            return dateTime;

        }
        catch
        {
            return default;
        }
    }
    public static void SaveTimeIsClickedRateLater(DateTime time)
    {
        string path = Application.persistentDataPath + "/TimeIsClickedRateLater.cub";
        List<int> list = new List<int>
        {
            time.Year,
            time.Month,
            time.Day,
            time.Hour,
            time.Minute,
            time.Second
        };
        Save(path, list);
    }
    public static DateTime? LoadTimeIsClickedRateLater()
    {
        string path = Application.persistentDataPath + "/TimeIsClickedRateLater.cub";
        try
        {
            if (File.Exists(path))
            {
                List<int> list = Load<List<int>>(path);
                DateTime dateTime = new DateTime(list[0], list[1], list[2], list[3], list[4], list[5]);
                return dateTime;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    private class StringComparerAsInt : IComparer<string>
    {
        public int Compare(string FirstStr, string SecondStr)
        {
            return System.Convert.ToInt32(SecondStr).CompareTo(System.Convert.ToInt32(FirstStr));
        }
    }
}
