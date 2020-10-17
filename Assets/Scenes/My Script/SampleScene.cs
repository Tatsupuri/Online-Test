using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourではなくMonoBehaviourPunCallbacksを継承して、Photonのコールバックを受け取れるようにする
public class SampleScene : MonoBehaviourPunCallbacks
{
    public string player_prefab;
    public Transform spawn_point;

    private void Start()
    {
        PhotonNetwork.Instantiate(player_prefab, spawn_point.position, spawn_point.rotation);
    }
    //private void Start() {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
    //    PhotonNetwork.ConnectUsingSettings();
    //}

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    //public override void OnConnectedToMaster() {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
    //    PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    //}

    // マッチングが成功した時に呼ばれるコールバック
    //public override void OnJoinedRoom() {
        // マッチング後、ランダムな位置に自分自身のネットワークオブジェクトを生成する
}
