using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	private bool _canShoot = true;
	private bool _grounded = true;
	private bool _increase = true;
	private bool _startMeter = false;
	private Club _currentClub;
	private float _forceY = 0.0f;
	private float _forceZ = 0.0f;
	private FlyCam _camera;
	private Hole _targetHole;
	private int _clubNum = 0;
	private int _shots = 0;
	private Rigidbody _rgbd;
	private Vector3 _ballPos;
	private Vector3 _direction;
	private Vector3 _startPos;
	private Vector3 _hole1Pos;
	private Vector3 _prevPos;
	public Canvas _between;
	public Canvas _canvas;
	public Canvas _card;
	public Club iron;
	public Club putter;
	public Club wedge;
	public Club wood;
	public Hole hole1;
	public Hole hole2;
	public Hole hole3;
	public GameObject tee2;
	public GameObject tee3;

	public bool _hit = false;
	public bool _holeDone = false;
	private float forceIncrease = 100f;
	public float forceMaxY;
	public float forceMaxZ;
	public float forceMinY;
	public float forceMinZ;
	public GameObject arrow;
	public UnityEngine.UI.Image powerBar;
	public UnityEngine.UI.Text shotsText;
	public UnityEngine.UI.Text shotsHole1;
	public UnityEngine.UI.Text shotsHole2;
	public UnityEngine.UI.Text shotsHole3;
	public UnityEngine.UI.Text shotsBetween;
	public UnityEngine.UI.Text scoreBetween;
	public UnityEngine.UI.Text clubText;
	public UnityEngine.UI.Text holeNumBetween;
	public UnityEngine.UI.Text holeParBetween;
	public UnityEngine.UI.Text holeMain;
	public Transform target;

	// Use this for initialization
	void Start()
	{
		_rgbd = gameObject.GetComponent<Rigidbody>();
		_camera = GameObject.FindObjectOfType<FlyCam>();
		_currentClub = wood;
		LookAtTarget();
		_startPos = transform.position;
		_hole1Pos = transform.position;
		_ballPos = _startPos;
		_prevPos = _startPos;
		RotateCamera();
		RotateArrow();
		shotsText.text = "Shots: " + _shots;
		shotsHole1.text = "-";
		shotsHole2.text = "-";
		shotsHole3.text = "-";
		powerBar.fillAmount = 0;
		_targetHole = hole1;
		holeMain.text = "Hole: " + _targetHole.holeNumber + "\nPar: " + _targetHole.par;
	}

	// Update is called once per frame
	void Update()
	{
		_ballPos = transform.position;
		float zVel = transform.InverseTransformDirection(_rgbd.velocity).z;

		if (_ballPos.x > 456 || _ballPos.x < 50 || _ballPos.z < 40 || _ballPos.z >= 440)
		{
			_ballPos = _prevPos;
			transform.position = _ballPos;
			StopBall();
		}
		if (!_holeDone)
		{
			if (_hit && (zVel >= -1 && zVel <= 0 || zVel <= -0.0001))
				StopBall();
			if (AnyMoveKeyDown())
			{
				_camera.canMove = true;
				arrow.SetActive(false);
				_canvas.gameObject.SetActive(false);
			}
			if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				NextClub();
			}
			if (Input.GetKey(KeyCode.Tab))
				_card.gameObject.SetActive(true);
			else
				_card.gameObject.SetActive(false);
			if (_canShoot)
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					if (_camera.canMove)
					{
						_camera.canMove = false;
						arrow.SetActive(true);
						_canvas.gameObject.SetActive(true);
						RotateCamera();
					}
					else if (!_camera.canMove && !_startMeter)
						_startMeter = true;
					else if (!_camera.canMove && _startMeter)
					{
						_hit = true;
						_grounded = false;
						_canShoot = false;
						HitBall();
						_startMeter = false;
					}
				}
				if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.A))
					RotateBall(-50f);
				if (!_camera.canMove && !_hit && _grounded && Input.GetKey(KeyCode.D))
					RotateBall(50f);
				if (_startMeter)
					BetterBallMeter();
			}
		}
		else if (_holeDone && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
		{
			if (_targetHole.holeNumber == 1)
			{
				_targetHole.holeNumber = 1;
				_startPos = tee2.transform.position;
				transform.position = _startPos;
				_ballPos = _startPos;
				target = hole2.transform;
				_targetHole = hole2;
			}
			else if (_targetHole.holeNumber == 2)
			{
				_targetHole.holeNumber = 2;
				_startPos = tee3.transform.position;
				transform.position = _startPos;
				_ballPos = _startPos;
				target = hole3.transform;
				_targetHole = hole3;
			}
			else if (_targetHole.holeNumber == 3)
			{
				_targetHole.holeNumber = 1;
				_startPos = _hole1Pos;
				transform.position = _startPos;
				_ballPos = _startPos;
				target = hole1.transform;
				_targetHole = hole1;
			}
			holeMain.text = "Hole: " + _targetHole.holeNumber + "\nPar: " + _targetHole.par;
			_camera.canMove = false;
			_canShoot = true;
			_startMeter = false;
			_grounded = true;
			_hit = false;
			_shots = 0;
			_between.gameObject.SetActive(false);
			_canvas.gameObject.SetActive(true);
			_holeDone = false;
			ResetHole();
		}
	}

	private void NextClub()
	{
		_clubNum++;
		if (_clubNum >= 4)
			_clubNum = 0;
		if (_clubNum == 0)
			_currentClub = wood;
		if (_clubNum == 1)
			_currentClub = putter;
		if (_clubNum == 2)
			_currentClub = wedge;
		if (_clubNum == 3)
			_currentClub = iron;
		clubText.text = "Club: " + _currentClub.name;
	}

	private void StopBall()
	{
		_rgbd.velocity = Vector3.zero;
		_rgbd.freezeRotation = true;
		_hit = false;
		_canShoot = true;
		LookAtTarget();
		arrow.SetActive(true);
		_canvas.gameObject.SetActive(true);
		_camera.canMove = false;
		powerBar.fillAmount = 0;
		_prevPos = transform.position;
		RotateCamera();
		RotateArrow();
	}

	private void LookAtTarget()
	{
		Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
		transform.LookAt(targetPosition);
	}

	private void HitBall()
	{
		_rgbd.AddForce(transform.forward * _forceZ);
		_rgbd.AddForce(transform.up * _forceY);
		_shots++;
		_startMeter = false;
		MarkCard();
		shotsText.text = "Shots: " + _shots;
		_forceY = 0.0f;
		_forceZ = 0.0f;
	}

	private void RotateBall(float dir)
	{
		transform.Rotate(0, dir * Time.deltaTime, 0);
		RotateCamera();
		RotateArrow();
	}

	private void RotateArrow()
	{
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 112)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 112)
		{
			offsetY -= 1;
		}
		Vector3 offset = arrow.transform.position - _ballPos;
		arrow.transform.position = new Vector3(_ballPos.x, _ballPos.y, _ballPos.z) + (transform.forward * 50);
		arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y + offsetY, arrow.transform.position.z);
		arrow.transform.LookAt(new Vector3(_ballPos.x, _ballPos.y + offsetY, _ballPos.z));
		arrow.transform.Rotate(18, 0, 0);
	}

	private void RotateCamera()
	{
		float offsetY = 0.0f;
		while (transform.position.y + offsetY < 105)
		{
			offsetY += 1;
		}
		while (transform.position.y + offsetY > 105)
		{
			offsetY -= 1;
		}
		Vector3 camPos = _camera.transform.position;
		Vector3 offset = _camera.transform.position - _ballPos;
		_camera.transform.position = new Vector3(_ballPos.x, _ballPos.y, _ballPos.z) + (-transform.forward * 8);
		_camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y + offsetY, _camera.transform.position.z);
		if (camPos.y < 104)
			_camera.transform.position = new Vector3(_camera.transform.position.x, 104, _camera.transform.position.z);
		else if (camPos.y > 160)
			_camera.transform.position = new Vector3(_camera.transform.position.x, 160, _camera.transform.position.z);
		else if (camPos.x > 456)
			_camera.transform.position = new Vector3(456, _camera.transform.position.y, _camera.transform.position.z);
		else if (camPos.x < 50)
			_camera.transform.position = new Vector3(50, _camera.transform.position.y, _camera.transform.position.z);
		else if (camPos.z < 40)
			_camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, 40);
		else if (camPos.z >= 440)
			_camera.transform.position = new Vector3(transform.position.x, transform.position.y, 440);
		_camera.transform.LookAt(new Vector3(_ballPos.x, _ballPos.y + offsetY, _ballPos.z));
	}

	private void BetterBallMeter()
	{
		if (_currentClub.name == "Wedge")
		{
			forceIncrease = 10f;
		}
		else
			forceIncrease = 20f;
		if (_increase && _forceZ <= _currentClub.maxZ)
			_forceZ += forceIncrease;
		if (_increase && _forceZ > _currentClub.maxZ)
			_increase = false;
		if (!_increase && _forceZ >= _currentClub.minZ)
			_forceZ -= forceIncrease;
		if (!_increase && _forceZ < _currentClub.minZ)
			_increase = true;
		float forcePercentage = _forceZ / _currentClub.maxZ;
		powerBar.fillAmount = forcePercentage;
		_forceY = _currentClub.maxY * forcePercentage;

	}

	private bool AnyMoveKeyDown()
	{
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
			return true;
		return false;
	}

	private void ResetHole()
	{
		transform.position = _startPos;
		_ballPos = _startPos;
		_rgbd.velocity = Vector3.zero;
		_rgbd.freezeRotation = true;
		LookAtTarget();
		RotateCamera();
		RotateArrow();
		StopBall();
		powerBar.fillAmount = 0;
		_hit = false;
	}

	private void MarkCard()
	{
		if (_targetHole.holeNumber == 1)
			shotsHole1.text = "" + _shots;
		if (_targetHole.holeNumber == 2)
			shotsHole2.text = "" + _shots;
		if (_targetHole.holeNumber == 3)
			shotsHole3.text = "" + _shots;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Terrain")
			_grounded = true;
		if (other.gameObject.layer == 4)
			ResetHole();
		if (other.gameObject.tag == "Hole")
		{
			Hole hitHole = other.gameObject.GetComponent<Hole>();
			if (hitHole.holeNumber == _targetHole.holeNumber)
				HandleBetweenScreen();
		}
	}

	private void HandleBetweenScreen()
	{
		_canvas.gameObject.SetActive(false);
		_holeDone = true;
		shotsBetween.text = "Shots: " + _shots;
		if (_targetHole.par - _shots == 1)
			scoreBetween.text = "You got a birdie!";
		else if (_targetHole.par - _shots == 2)
			scoreBetween.text = "You got an eagle!";
		else if (_targetHole.par - _shots == 0)
			scoreBetween.text = "You got Par!";
		else if (_targetHole.par - _shots == -1)
			scoreBetween.text = "You got a Bogey!";
		else if (_targetHole.par - _shots == -2)
			scoreBetween.text = "You got a double Bogey!";
		else if (_targetHole.par - _shots == -3)
			scoreBetween.text = "You got a triple Bogey!";
		else
			scoreBetween.text = "You got a +" + ((_targetHole.par - _shots) * -1);
		holeNumBetween.text = "Hole: " + _targetHole.holeNumber;
		holeParBetween.text = "Par: " + _targetHole.par;
		_between.gameObject.SetActive(true);
	}

	private void OnCollisionExit(Collision other)
	{
		_forceY = 0.0f;
		_forceZ = 0.0f;
	}
}
