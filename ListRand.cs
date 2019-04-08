using System;
using System.Collections.Generic;
using System.IO;

namespace RandList
{    
    class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; 
        public string Data;        
    }


    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(string s)
        {
            
            Dictionary<ListNode, int> map = new Dictionary<ListNode, int>(10);           
            ListNode temp = Head;
            int index = 0;

            while (temp != null)
            {    
                map.Add(temp, index); // O(1)
                temp = temp.Next;
                ++index;
            } //O(n)
            
            using (StreamWriter w = new StreamWriter(s))
            {
                w.WriteLine(index);
                foreach(var node in map.Keys) // O(n)         
                    w.WriteLine("{0}:{1}", node.Data.ToString(), map[node.Rand].ToString());  // O(1)               
            }                
        }

        public void Deserialize(string s)
        {
            
            string fileStr;
            List<ListNode> list;
            try
            {
                using (StreamReader r = new StreamReader(s))
                {
                    Count = Int32.Parse(r.ReadLine());
                    list = new List<ListNode>(Count);                   
                    while ((fileStr = r.ReadLine()) != null)
                    {
                        ListNode node = new ListNode();
                        node.Data = fileStr;
                        list.Add(node);  //O(1) 
                    } //O(n) 
                }                    

                Head = list[0];
                Tail = list[Count-1];
                ListNode prevNode = null;
                foreach (ListNode node in list)
                {                    
                    node.Prev = prevNode;
                    if (prevNode != null)
                        prevNode.Next = node;
                    int randIndex = Int32.Parse(node.Data.Split(':')[1]);
                    node.Data = node.Data.Split(':')[0];
                    node.Rand = list[randIndex];  //O(1) 
                    prevNode = node;
                } //O(n) 
            }
            catch (Exception e)
            {
                Console.WriteLine("Неверный формат файла:");
                Console.WriteLine(e.Message);
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                Environment.Exit(0);
            }
        }
    }
}
