using UnityEngine;

public class FlootIsLava_LevelController : MonoBehaviour, LevelControllerInterface
{
    public Transform lavaTransform;
    public float lavaSpeed;
    public float lavaMaxHeight;
    public float lavaStartHeight;

    public bool levelStarted;

    void Update()
    {
        lavaTransform.Translate(Vector2.up * lavaSpeed *  Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector2.up * lavaMaxHeight, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Vector2.up * lavaStartHeight, 0.5f);
    }

    [ContextMenu("Start Level")]
    public void StartLevel()
    {
        lavaTransform.transform.position = new Vector2(lavaTransform.position.x, lavaStartHeight - (lavaTransform.localScale.y * 0.5f));
        levelStarted = true;
    }

    public void FinishLevel()
    {
        levelStarted = false;
    }

    public void PauseLevel()
    {
    }

    public void ComproveLevelFinished()
    {
        throw new System.NotImplementedException();
    }
}
