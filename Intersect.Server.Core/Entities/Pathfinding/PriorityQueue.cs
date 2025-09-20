namespace Intersect.Server.Entities.Pathfinding;


internal partial class PriorityQueue<T> where T : IIndexedObject
{

    protected IComparer<T> mComparer;

    protected List<T> mInnerList = new List<T>();

    public PriorityQueue()
    {
        mComparer = Comparer<T>.Default;
    }

    public PriorityQueue(IComparer<T> comparer)
    {
        mComparer = comparer;
    }

    public PriorityQueue(IComparer<T> comparer, int capacity)
    {
        mComparer = comparer;
        mInnerList.Capacity = capacity;
    }

    public int Count => mInnerList.Count;

    protected void SwitchElements(int i, int j)
    {
        var h = mInnerList[i];
        mInnerList[i] = mInnerList[j];
        mInnerList[j] = h;

        mInnerList[i].Index = i;
        mInnerList[j].Index = j;
    }

    protected virtual int OnCompare(int i, int j)
    {
        return mComparer.Compare(mInnerList[i], mInnerList[j]);
    }

    /// <summary>
    ///     Push an object onto the PQ
    /// </summary>
    /// <param name="item">The new object</param>
    /// <returns>
    ///     The index in the list where the object is _now_. This will change when objects are taken from or put onto the
    ///     PQ.
    /// </returns>
    public int Push(T item)
    {
        int p = mInnerList.Count, p2;
        item.Index = mInnerList.Count;
        mInnerList.Add(item); // E[p] = O

        do
        {
            if (p == 0)
            {
                break;
            }

            p2 = (p - 1) / 2;
            if (OnCompare(p, p2) < 0)
            {
                SwitchElements(p, p2);
                p = p2;
            }
            else
            {
                break;
            }
        } while (true);

        return p;
    }

    /// <summary>
    ///     Get the smallest object and remove it.
    /// </summary>
    /// <returns>The smallest object</returns>
    public T Pop()
    {
        var result = mInnerList[0];
        int p = 0, p1, p2, pn;

        mInnerList[0] = mInnerList[mInnerList.Count - 1];
        mInnerList[0].Index = 0;

        mInnerList.RemoveAt(mInnerList.Count - 1);

        result.Index = -1;

        do
        {
            pn = p;
            p1 = 2 * p + 1;
            p2 = 2 * p + 2;
            if (mInnerList.Count > p1 && OnCompare(p, p1) > 0) // links kleiner
            {
                p = p1;
            }

            if (mInnerList.Count > p2 && OnCompare(p, p2) > 0) // rechts noch kleiner
            {
                p = p2;
            }

            if (p == pn)
            {
                break;
            }

            SwitchElements(p, pn);
        } while (true);

        return result;
    }

    /// <summary>
    ///     Notify the PQ that the object at position i has changed
    ///     and the PQ needs to restore order.
    /// </summary>
    public void Update(T item)
    {
        var count = mInnerList.Count;
        var index = item.Index;

        if (index < 0 || index >= count)
        {
            return;
        }

        // Bubble up until the heap invariant is restored with the parent node.
        while (index > 0)
        {
            var parentIndex = (index - 1) / 2;

            if (OnCompare(index, parentIndex) < 0)
            {
                SwitchElements(index, parentIndex);
                index = parentIndex;
            }
            else
            {
                break;
            }
        }

        // Bubble down to ensure the children also respect the heap invariant.
        while (true)
        {
            var leftChild = index * 2 + 1;
            if (leftChild >= count)
            {
                break;
            }

            var rightChild = leftChild + 1;
            var smallestChild = leftChild;

            if (rightChild < count && OnCompare(rightChild, leftChild) < 0)
            {
                smallestChild = rightChild;
            }

            if (OnCompare(index, smallestChild) > 0)
            {
                SwitchElements(index, smallestChild);
                index = smallestChild;
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    ///     Get the smallest object without removing it.
    /// </summary>
    /// <returns>The smallest object</returns>
    public T Peek()
    {
        if (mInnerList.Count > 0)
        {
            return mInnerList[0];
        }

        return default(T);
    }

    public void Clear()
    {
        mInnerList.Clear();
    }

}
