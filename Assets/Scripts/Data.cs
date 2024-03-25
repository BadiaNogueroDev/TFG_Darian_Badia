using UnityEngine;

public class Data : MonoBehaviour
{
    private int hitsReceived;
    private int magazineChanged;
    private int shotsFired;
    private int enemiesKilled;
    private int objectsGrabbed;
    
    public int HitsReceived { get { return hitsReceived;} set { hitsReceived = value; } }
    public int MagazineChanged { get { return magazineChanged;} set { magazineChanged = value; } }
    public int ShotsFired { get { return shotsFired;} set { shotsFired = value; } }
    public int EnemiesKilled { get { return enemiesKilled;} set { enemiesKilled = value; } }
    public int ObjectsGrabbed { get { return objectsGrabbed;} set { objectsGrabbed = value; } }
}