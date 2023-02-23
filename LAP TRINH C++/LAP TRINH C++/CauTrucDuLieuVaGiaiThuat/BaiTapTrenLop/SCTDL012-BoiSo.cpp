#include<bits/stdc++.h>

using namespace std;

// Hàm tìm số X nhỏ nhất là bội của N, chỉ chứa hai chữ số 0 và 9
int res[505];
bool visited[505];
void Init()
{
    int dem = 0;
    queue<int> q;
    q.push(9);
    while (dem < 500)
    {
        int t = q.front();
        q.pop();
        for(int i =1 ; i <= 500; i++)
        {
            if(t%i == 0 && !visited[i])
            {
                visited[i] = true;
                res[i] = t;
                dem++;
            }
        }
        q.push(t*10);
        q.push(t*10+9);
    }
}


int main() {
    int T;
    cin >> T;
    Init();
    while (T--) {
        int N;
        cin >> N;
        cout << res[N] << endl;
    }
    return 0;
}
