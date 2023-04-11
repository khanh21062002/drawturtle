#include<bits/stdc++.h>
#define faster() ios_base::sync_with_stdio(false), cin.tie(0), cout.tie(0);

using namespace std;

string n, k;
bool check(string s)
{
	return s.size() < n.size() || (s.size() == n.size() && s <= n);
}
int main()
{
    faster();
	int t;
	cin >> t;
	while (t--)
	{
		int d = 0;
		cin >> n;
		stack<string> st;
		st.push("1");
		while (st.size())
		{
			k = st.top();
			d++;
			st.pop();
			if (check(k + "0"))
				st.push(k + "0");
			if (check(k + "1"))
				st.push(k + "1");
		}
		cout << d << endl;
	}
}