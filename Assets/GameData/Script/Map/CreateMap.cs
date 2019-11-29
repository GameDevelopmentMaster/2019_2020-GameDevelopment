using System.Collections.Generic;

public class MapNode
{
    
    private MapNode leftNode;
    private MapNode rightNode;
    private MapNode parentNode;
    private MapNode nextParentNode;

    private ref MapNode NextParentNode => ref nextParentNode;


    string nameData;
    MapNode GetLeftLastNode()
    {
        return leftNode.leftNode == null ? leftNode : leftNode.GetLeftLastNode();
    }
    MapNode GetRightLastNode()
    {
        return rightNode.rightNode == null ? rightNode : rightNode.GetRightLastNode();
    }
   public MapNode GetLeftNode()
    {
        return leftNode.leftNode == null ? leftNode : leftNode.leftNode;
    }
    public MapNode GetRIghtNode()
    {
        return rightNode.rightNode == null ? rightNode : rightNode.rightNode;
    }
    public  MapNode GetParentNode()
    {
        return parentNode;
    }

    Data nodeData = new Data();
    public ref Data NodeData
    { 
        get { return ref nodeData; }
        
    }
    public MapNode()
    {
        nodeData.x = nodeData.y = 0f;
        nodeData.width = 300f;
        nodeData.height = 200f;
        parentNode = this;
    }

    public MapNode(Data size)
    {
        nodeData.x = size.x;
        nodeData.y = size.y;
        nodeData.width = size.width;
        nodeData.height = size.height;
        
    }
    public MapNode(MapNode ParentNode)
    {
        parentNode = ParentNode;
    }

    public void AddChild(MapNode ParentNode)
    {
        ParentNode.rightNode = new MapNode(ParentNode);
        ParentNode.leftNode = new MapNode(ParentNode);
        ParentNode.leftNode.NextParentNode = ParentNode.rightNode;
    }

    
    public void AddNode()
    {
        if(rightNode == null && leftNode == null)
        {
            rightNode = new MapNode(parentNode);
            leftNode = new MapNode(parentNode);
            leftNode.NextParentNode = rightNode;
                
        }
        else
        {
            MapNode getNextNode = GetLeftLastNode();
            MapNode getRightNode =GetRightLastNode(),  checkNode = GetRightLastNode();
            while(getNextNode != checkNode.nextParentNode)
            {
                AddChild(getNextNode);
                getRightNode.nextParentNode = getNextNode.leftNode;
                getRightNode = getNextNode.rightNode;
                getNextNode = getNextNode.NextParentNode;
            }
        }
    }
    
    public void MapCut(int count)
    {
        for(int i=0; i<count; i++)
        {
            if (GetParentNode() != null)
            {
                AddNode();
                switch ((Dir)UnityEngine.Random.Range(0, 2))
                {
                    case Dir.horizontal:
                        HorizontalCut();
                        break;

                    case Dir.vertical:
                        VerticalCut();
                        break;
                }
            }

        }
        
    }
    /// <summary>
    /// Cut Map Data is Vertical
    /// </summary>
    void VerticalCut() 
    {
        MapNode cutNode = GetLeftLastNode().parentNode;
        MapNode checkNode = GetRightLastNode().parentNode;

        while (cutNode != checkNode.NextParentNode)
        {

            cutNode.leftNode.NodeData.x = cutNode.rightNode.NodeData.x = cutNode.NodeData.x;
            cutNode.leftNode.NodeData.width = cutNode.rightNode.NodeData.width = cutNode.NodeData.width;

            cutNode.leftNode.NodeData.height = UnityEngine.Random.Range(cutNode.NodeData.height * 0.3f, cutNode.NodeData.height * 0.7f);
            cutNode.leftNode.NodeData.y = cutNode.NodeData.y - (cutNode.NodeData.height - cutNode.leftNode.NodeData.height) / 2f;

            cutNode.rightNode.NodeData.height = cutNode.NodeData.height - cutNode.leftNode.NodeData.height;
            cutNode.rightNode.NodeData.y = cutNode.NodeData.y + (cutNode.NodeData.height - cutNode.rightNode.NodeData.height) / 2f;



            cutNode = cutNode.NextParentNode;
        }

        #region Old Code

        #region 0.2Ver
        //MapNode cutNodeData = GetRightLastNode().parentNode;

        //cutNodeData.rightNode.NodeData.x = cutNodeData.leftNode.NodeData.x = cutNodeData.NodeData.x;
        //cutNodeData.rightNode.NodeData.width = cutNodeData.leftNode.NodeData.width = cutNodeData.NodeData.width;

        //cutNodeData.rightNode.NodeData.height = UnityEngine.Random.Range(cutNodeData.NodeData.height * 0.3f, cutNodeData.NodeData.height * 0.7f);                                 /// 처리할 노드의 오른쪽 자식 노드 높이값
        //cutNodeData.rightNode.NodeData.y = cutNodeData.NodeData.y - (cutNodeData.NodeData.height - cutNodeData.rightNode.NodeData.height) / 2f;                                   /// 처리할 노드의 오른쪽 자식 노드 y축값

        //cutNodeData.leftNode.NodeData.height = cutNodeData.NodeData.height - cutNodeData.rightNode.NodeData.height;                                                               /// 처리할 노드의 왼쪽 자식 노드 높이값
        //cutNodeData.leftNode.NodeData.y = cutNodeData.NodeData.y + (cutNodeData.NodeData.height - cutNodeData.leftNode.NodeData.height) / 2f;                                     /// 처리할 노드의 왼쪽 자식 노드 y축값

        //if(GetRightLastNode().parentNode != GetLeftLastNode().parentNode)
        //{
        //    cutNodeData = GetLeftLastNode().parentNode;

        //    cutNodeData.rightNode.NodeData.x = cutNodeData.leftNode.NodeData.x = cutNodeData.NodeData.x;
        //    cutNodeData.rightNode.NodeData.width = cutNodeData.leftNode.NodeData.width = cutNodeData.NodeData.width;

        //    cutNodeData.rightNode.NodeData.height = UnityEngine.Random.Range(cutNodeData.NodeData.height * 0.3f, cutNodeData.NodeData.height * 0.7f);                                 /// 처리할 노드의 오른쪽 자식 노드 높이값
        //    cutNodeData.rightNode.NodeData.y = cutNodeData.NodeData.y - (cutNodeData.NodeData.height - cutNodeData.rightNode.NodeData.height) / 2f;                                   /// 처리할 노드의 오른쪽 자식 노드 y축값

        //    cutNodeData.leftNode.NodeData.height = cutNodeData.NodeData.height - cutNodeData.rightNode.NodeData.height;                                                               /// 처리할 노드의 왼쪽 자식 노드 높이값
        //    cutNodeData.leftNode.NodeData.y = cutNodeData.NodeData.y + (cutNodeData.NodeData.height - cutNodeData.leftNode.NodeData.height) / 2f;                                     /// 처리할 노드의 왼쪽 자식 노드 y축값

        //}
        #endregion

        #region 0.1Ver
        //    Data rightParentNodeData = GetRIghtNode().GetParentNode().NodeData;
        //    Data leftParentNodeData = GetLeftLastNode().GetParentNode().NodeData;

        //GetRightLastNode().NodeData.x = GetLeftLastNode().NodeData.x = GetParentNode().NodeData.x;
        //GetRightLastNode().NodeData.width = GetLeftLastNode().NodeData.width = GetParentNode().NodeData.width;

        //GetRightLastNode().NodeData.height = UnityEngine.Random.Range(GetParentNode().NodeData.height * 0.3f, GetParentNode().NodeData.height * 0.7f);
        //GetRightLastNode().NodeData.y = GetParentNode().NodeData.y - (GetParentNode().NodeData.height - GetRightLastNode().NodeData.height)/2f;

        //GetLeftLastNode().NodeData.height = GetParentNode().NodeData.height - GetRightLastNode().NodeData.height;
        //GetLeftLastNode().NodeData.y = GetParentNode().NodeData.y + (GetParentNode().NodeData.height - GetLeftLastNode().NodeData.height)/2f;
        #endregion
        #endregion
    }


    /// <summary>
    /// Cut Map Data is Horizontal
    /// </summary>
    void HorizontalCut()
    {
        MapNode cutNode = GetLeftLastNode().parentNode;
        MapNode checkNode = GetRightLastNode().parentNode;

        while(cutNode != checkNode.NextParentNode)
        {
            cutNode.leftNode.NodeData.y = cutNode.rightNode.NodeData.y = cutNode.NodeData.y;
            cutNode.leftNode.NodeData.height = cutNode.rightNode.NodeData.height = cutNode.NodeData.height;

            cutNode.leftNode.NodeData.width = UnityEngine.Random.Range(cutNode.NodeData.width * 0.3f, cutNode.NodeData.width * 0.7f);
            cutNode.leftNode.NodeData.x = cutNode.NodeData.x - (cutNode.NodeData.width - cutNode.leftNode.NodeData.width) / 2f;

            cutNode.rightNode.NodeData.width = cutNode.NodeData.width - cutNode.leftNode.NodeData.width;
            cutNode.rightNode.NodeData.x = cutNode.NodeData.x + (cutNode.NodeData.width - cutNode.rightNode.NodeData.width)/2f;

            cutNode = cutNode.nextParentNode;
        }

        #region OldCode

        #region 0.2ver
        //MapNode cutNodeData = GetRightLastNode().parentNode;

        //cutNodeData.rightNode.NodeData.y = cutNodeData.leftNode.NodeData.y = cutNodeData.NodeData.y;
        //cutNodeData.rightNode.NodeData.height = cutNodeData.leftNode.NodeData.height = cutNodeData.NodeData.height;

        //cutNodeData.rightNode.NodeData.width = UnityEngine.Random.Range(cutNodeData.NodeData.width * 0.3f, cutNodeData.NodeData.width * 0.7f);                                 /// 처리할 노드의 오른쪽 자식 노드 높이값
        //cutNodeData.rightNode.NodeData.x = cutNodeData.NodeData.x - (cutNodeData.NodeData.width - cutNodeData.rightNode.NodeData.width) / 2f;                                   /// 처리할 노드의 오른쪽 자식 노드 y축값

        //cutNodeData.leftNode.NodeData.width = cutNodeData.NodeData.width - cutNodeData.rightNode.NodeData.width;                                                               /// 처리할 노드의 왼쪽 자식 노드 높이값
        //cutNodeData.leftNode.NodeData.x = cutNodeData.NodeData.x + (cutNodeData.NodeData.width - cutNodeData.leftNode.NodeData.width) / 2f;                                     /// 처리할 노드의 왼쪽 자식 노드 y축값

        //if (GetRightLastNode().parentNode != GetLeftLastNode().parentNode)
        //{
        //    cutNodeData = GetLeftLastNode().parentNode;

        //    cutNodeData.rightNode.NodeData.y = cutNodeData.leftNode.NodeData.y = cutNodeData.NodeData.y;
        //    cutNodeData.rightNode.NodeData.height = cutNodeData.leftNode.NodeData.height = cutNodeData.NodeData.height;

        //    cutNodeData.rightNode.NodeData.width = UnityEngine.Random.Range(cutNodeData.NodeData.width * 0.3f, cutNodeData.NodeData.width * 0.7f);                                 /// 처리할 노드의 오른쪽 자식 노드 높이값
        //    cutNodeData.rightNode.NodeData.x = cutNodeData.NodeData.x - (cutNodeData.NodeData.width - cutNodeData.rightNode.NodeData.width) / 2f;                                   /// 처리할 노드의 오른쪽 자식 노드 y축값

        //    cutNodeData.leftNode.NodeData.width = cutNodeData.NodeData.width - cutNodeData.rightNode.NodeData.width;                                                               /// 처리할 노드의 왼쪽 자식 노드 높이값
        //    cutNodeData.leftNode.NodeData.x = cutNodeData.NodeData.x + (cutNodeData.NodeData.width - cutNodeData.leftNode.NodeData.width) / 2f;                                     /// 처리할 노드의 왼쪽 자식 노드 y축값

        //}
        #endregion

        #region 0.1Ver
        //GetRightLastNode().NodeData.y = GetLeftLastNode().NodeData.y = GetParentNode().NodeData.y;
        //GetRightLastNode().NodeData.height = GetLeftLastNode().NodeData.height = GetParentNode().NodeData.height;

        //GetLeftLastNode().NodeData.width = UnityEngine.Random.Range(GetParentNode().NodeData.width * 0.3f, GetParentNode().NodeData.width * 0.7f);
        //GetLeftLastNode().NodeData.x = GetParentNode().NodeData.x - (GetParentNode().NodeData.width - GetLeftLastNode().NodeData.width)/2f;

        //GetRightLastNode().NodeData.width = GetParentNode().NodeData.width - GetLeftLastNode().NodeData.width;
        //GetRightLastNode().NodeData.x = GetParentNode().NodeData.x + (GetParentNode().NodeData.width - GetRightLastNode().NodeData.width)/2f;
        #endregion

        #endregion
    }

    private Data[] GetNodeData()
    {
        MapNode checkNode = GetLeftLastNode();

        List<Data> getNodeData = new List<Data>();
        while(checkNode != null)
        {
            getNodeData.Add(checkNode.nodeData);
            checkNode = checkNode.nextParentNode;
        }
        return getNodeData.ToArray();
    }

    public UnityEngine.Vector3[] SetPos()
    {
        UnityEngine.Vector3[] valuePos = new UnityEngine.Vector3[GetNodeData().Length];

        for(int i=0; i<valuePos.Length; i++)
        {
            valuePos[i] = new UnityEngine.Vector3();
            valuePos[i].x = GetNodeData()[i].x;
            valuePos[i].z = GetNodeData()[i].y;
        }
        return valuePos;
    }

    public UnityEngine.Vector3[] SetSize()
    {
        UnityEngine.Vector3[] valueSize = new UnityEngine.Vector3[GetNodeData().Length];
        for(int i=0; i<valueSize.Length; i++)
        {
            valueSize[i] = new UnityEngine.Vector3();
            valueSize[i].x = GetNodeData()[i].width;
            valueSize[i].y = 11f;
            valueSize[i].z = GetNodeData()[i].height;
        }
        return valueSize;
    }

   public struct Data
    {
        public float x;
        public float y;
        public float width;
        public float height;

        public void SetData(Data setValue)
        {
            x = setValue.x;
            y = setValue.y;
            width = setValue.width;
            height = setValue.height;
        }
    }
    
}



enum Dir
{
    vertical, horizontal
};
