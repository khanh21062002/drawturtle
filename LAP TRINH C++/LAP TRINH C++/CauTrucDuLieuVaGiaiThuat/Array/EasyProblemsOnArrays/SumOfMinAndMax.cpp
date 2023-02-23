//{ Driver Code Starts
/* Driver program to test above function */

#include<bits/stdc++.h>
using namespace std;

// } Driver Code Ends
//User function Template for C++

class Solution
{
   public:
    int findSum(int A[], int N)
    {
        int min,max;
        int sum_of_max_min = 0;
        if (N == 1)
        {
            min = max = A[0];
            sum_of_max_min = min + max;
            return sum_of_max_min;
        }
        if(A[0] < A[1])
        {
            min = A[0];
            max = A[1];
        }
        else
        {
            min = A[1];
            max = A[0];
        }
        for(int i = 2 ; i < N ; i++)
        {
            if(A[i] > max )
            {
                max = A[i];
            }
            else if (A[i] < min)
            {
                min = A[i];
            }
        }
        sum_of_max_min = min + max;
        return sum_of_max_min;
    }
};

//{ Driver Code Starts.
int main()
{
	int t;
	cin>>t;
	while(t--)
	{
	    int n;
	    cin>>n;
	    int arr[n];
	    for(int i=0;i<n;i++)
	      cin>>arr[i];
	    Solution ob;  
	    int ans=ob.findSum(arr, n);
	    cout<<ans;
	    cout<<"\n";
	}
	return 0;
}

// } Driver Code Ends