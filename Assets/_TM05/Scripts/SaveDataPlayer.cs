using System;
using System.Collections.Generic;
using System.IO;
using Pixelplacement;
using UnityEngine;

[System.Serializable]
public class DataItem
{
    // Khởi tạo danh sách ngay từ đầu để tránh lỗi null
    public List<item> items = new List<item>();
}

[System.Serializable]
public class item
{
    public int key;
    public float value;
}

public class SaveDataPlayer : Singleton<SaveDataPlayer>
{
    public DataItem dateItem;
    private string filePath;

    // Các biến để test trong Editor
    public int key;
    public float value;

    public void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "NicePuzzleSave.json");
        LoadData();
    }

    public void Start()
    {
        Save(1, 3);
        SaveData();
    }

    // QUAN TRỌNG: Thêm hàm này để lưu dữ liệu khi ứng dụng bị tạm dừng trên di động
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveData();
        }
    }

    // Update chỉ nên dùng để test trong Editor
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Save Test");
            Save(key, value);
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Load Test");
            Value(key);
        }
        // if (Input.GetKeyUp(KeyCode.Alpha3))
        // {
        //     ResetData();
        //     SaveData(); // Lưu lại trạng thái đã reset
        //     Debug.Log("Data Reset and Saved");
        // }
    }

    public void Save(int key, float value)
    {
        // Tìm và cập nhật item đã có
        foreach (var nameKey in dateItem.items)
        {
            if (nameKey.key == key)
            {
                nameKey.value = value;
                SaveData(); // Lưu lại dữ liệu sau khi thay đổi
                return;
            }
        }

        // Nếu không tìm thấy, tạo item mới
        item newItem = new item();
        newItem.key = key;
        newItem.value = value;
        dateItem.items.Add(newItem);
        
        SaveData(); // Lưu lại dữ liệu sau khi thêm mới
    }

    public float Value(int key)
    {
        foreach (var nameKey in dateItem.items)
        {
            if (nameKey.key == key)
            {
                //Debug.Log("Loaded key: " + key + ", value: " + nameKey.value);
                return nameKey.value;
            }
        }
        //Debug.Log("Key not found: " + key + ". Returning default value 0.");
        return 0;
    }

    public void LoadData()
    {
        // SỬA LỖI: Logic tải dữ liệu đã được sửa lại
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            // Kiểm tra xem file có rỗng không trước khi parse
            if (!string.IsNullOrEmpty(json))
            {
                dateItem = JsonUtility.FromJson<DataItem>(json);
                Debug.Log("Load Done from path: " + filePath);
                // Đảm bảo list items không bao giờ bị null sau khi load
                if (dateItem.items == null)
                {
                    dateItem.items = new List<item>();
                }
                return;
            }
        }
        
        // Nếu file không tồn tại hoặc rỗng, tạo dữ liệu mới
        Debug.LogWarning("Save file not found or empty. Creating new data.");
        ResetData();
        SaveData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(dateItem, true);
        File.WriteAllText(filePath, json);
        Debug.Log("File saved at path: " + filePath);
    }
    
    public void ResetData()
    {
        // SỬA LỖI: Hoàn thiện hàm ResetData
        dateItem = new DataItem();
        // Không cần khởi tạo list ở đây vì đã làm trong constructor của DataItem
    }
}