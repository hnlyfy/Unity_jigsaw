using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
	public Texture2D mainTexture;
	public GameObject uiTexturePrefeb;
	int indexX;
	int indexY;
	Blocks mBlocks;
	GameObject target;
	// Use this for initialization
	void Start ()
	{

		target = GameObject.FindWithTag ("textures");

		indexX = 3;
		indexY = 3;

		mBlocks = new Blocks (mainTexture, indexX, indexY);
		List<Block> blocks = mBlocks.getBlockList ();

		UITexture UIT;
		for (int i = 0; i < blocks.Count; i++) {
			GameObject go = AddChild (target, null, uiTexturePrefeb);
			go.transform.localPosition = blocks [i].position;

			UIT = go.GetComponentInChildren<UITexture> ();
			UIT.mainTexture = mainTexture;
			UIT.uvRect = blocks [i].rect;
			UIT.width = blocks [i].sizeX;
			UIT.height = blocks [i].sizeY;

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public class Blocks
	{

//		private Texture2D _tex;
		private int _rows;
		private int _cols;
		private int _sizeX;
		private int _sizeY;
		private List<Block> _blockList;

		public Blocks (Texture2D tex, int rows = 4, int columns = 4)
		{
//			this._tex = tex;
			this._rows = Mathf.Clamp (rows, 2, 10);
			this._cols = Mathf.Clamp (columns, 2, 10);
			this._sizeX = Mathf.CeilToInt (tex.width / this._rows);
			this._sizeY = Mathf.CeilToInt (tex.height / this._cols);
			this._blockList = new List<Block> ();
			this.setBlockArr ();
		}

		public int getSizeX {
			get {
				return _sizeX;
			}
		}

		public int getSizeY {
			get {
				return _sizeY;
			}
		}

		private void setBlockArr ()
		{
			Block tBlock;
			for (int i = 0; i < this._rows; i++) {
				for (int j = 0; j<this._cols; j++) {
					tBlock = new Block (i, j, this._rows, this._cols, this._sizeX, this._sizeY);
					this._blockList.Add (tBlock);
				}
			}
		}

		public List<Block> getBlockList ()
		{
			return this._blockList;
		}

	}

	public class Block
	{

		public Rect rect;
		public float UVRectX;
		public float UVRectY;
		public float UVRectWidth;
		public float UVRectHeight;
		public int sizeX;
		public int sizeY;
		public int posX;
		public int posY;
		public Vector3 position;

		public Block (int indexX, int indexY, int countX, int countY, int sizeX, int sizeY)
		{
			this.UVRectX = (float)indexX / countX;
			this.UVRectWidth = (float)1 / countX;
			this.UVRectY = (float)indexY / countY;
			this.UVRectHeight = (float)1 / countY;
			this.rect = new Rect (this.UVRectX, this.UVRectY, this.UVRectWidth, this.UVRectHeight);

			this.sizeX = sizeX;
			this.sizeY = sizeY;

			this.posX = (indexX - countX / 2) * sizeX-sizeX/2;
			this.posY = (indexY - countX / 2) * sizeY;
			this.position = new Vector3 (this.posX, this.posY);
		}
	}

	public static GameObject AddChild (GameObject pParent, string pName = null, GameObject pPrefab = null)
	{
		GameObject go = pPrefab == null ? new GameObject () : GameObject.Instantiate (pPrefab) as GameObject;
		if (!string.IsNullOrEmpty (pName))
			go.name = pName;
		
		if (pParent != null) {
			Transform t = go.transform;
			t.parent = pParent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = pParent.layer;
		}
		return go;
	}

}
