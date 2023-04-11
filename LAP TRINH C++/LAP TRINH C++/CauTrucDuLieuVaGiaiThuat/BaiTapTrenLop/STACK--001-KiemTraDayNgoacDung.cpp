#include<bits/stdc++.h>
#define ll long long
#define faster() ios_base::sync_with_stdio(false),cin.tie(0),cout.tie(0);

using namespace std;

bool K_tra_day_ngoac_dung(string s) {
    stack<char> st;
    for (char c : s) {
        if (c == '(' || c == '[' || c == '{') {
            st.push(c);
        } else if (c == ')') {
            if (st.empty() || st.top() != '(') {
                return false;
            }
            st.pop();
        } else if (c == ']') {
            if (st.empty() || st.top() != '[') {
                return false;
            }
            st.pop();
        } else if (c == '}') {
            if (st.empty() || st.top() != '{') {
                return false;
            }
            st.pop();
        }
    }
    return st.empty();
}

int main()
{
    faster();
    int t;
    cin >> t;
    while (t--)
    {
        string s;
        cin >> s;
        if(K_tra_day_ngoac_dung(s))
        {
            cout << "YES" << endl;
        }
        else{
            cout << "NO" << endl;
        }
    }
    return 0;
}