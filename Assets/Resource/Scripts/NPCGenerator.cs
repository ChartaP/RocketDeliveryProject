using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    private static NPCGenerator instance = null;
    public Transform ObjectTrans = null;
    [SerializeField]
    private List<GameObject> NPC_List = new List<GameObject>();
    [SerializeField]
    private int nMaxNPC = 100;
    [SerializeField]
    private Vector3 generateRange = new Vector3(100,100,100);

    [SerializeField]
    private List<GameObject> GeneratedCharList = new List<GameObject>();

    private int nCount = 0;
    public static NPCGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(NPCGenerator)) as NPCGenerator;
            }
            return instance;
        }
    }

    private void Start()
    {
        StartCoroutine("NPC_Generator");
    }

    public void UnregNPC(GameObject NPC)
    {
        GeneratedCharList.Remove(NPC);
    }

    IEnumerator NPC_Generator()
    {
        while (true)
        {
            int nSchedule = nMaxNPC - GeneratedCharList.Count;
            for(int n = 0;n< nSchedule; n++)
            {
                StartCoroutine("NPC_Instansiate");
            }
            yield return null;
        }
        yield break;
    }

    IEnumerator NPC_Instansiate()
    {
        Vector3 OriginPos ;
        Vector3 RandomPos;
        
        while (true)
        {
            RaycastHit hit;
            OriginPos = PlayerCtrl.Instance.CurTrans.position;
            RandomPos.y = 600f;
            RandomPos.x = Random.Range(OriginPos.x - generateRange.x, OriginPos.x + generateRange.x);

            RandomPos.z = Random.Range(OriginPos.z - generateRange.z, OriginPos.z + generateRange.z);
            if ( Mathf.Abs( RandomPos.x) < 50f)
            {
                RandomPos.z += (RandomPos.z > OriginPos.z ? 50f : -50f);
            }
            else if(Mathf.Abs(RandomPos.z) < 50f)
            {
                RandomPos.x += (RandomPos.z > OriginPos.z ? 50f : -50f);
            }

            if (Physics.BoxCast(RandomPos,new Vector3(1f,1f,1f),Vector3.down,out hit, transform.rotation, 800f))
            {
                if(hit.transform.tag != "Building")
                {
                    GameObject NPC = Instantiate(NPC_List[Random.Range(0,NPC_List.Count)],hit.point+Vector3.up,transform.rotation,ObjectTrans);
                    NPC.name += nCount++;
                    GeneratedCharList.Add(NPC);
                    break;
                }
            }

            yield return null;
        }
        yield break;
    }

}
