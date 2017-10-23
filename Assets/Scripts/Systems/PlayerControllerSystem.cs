using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using Unitilities.Tuples;

public class PlayerControllerSystem : MonoBehaviour {
  public float speed; // Floating point variable to store the player's
                      // movement speed.
  public bool underControl = true;

  // Stores the current player, changes when a new one is spawned
  public GameObject currentPlayer;
  public float cooldown       = 0.1f;
  public float transitionTime = 0.2f;
  public State state          = State.walking;
  public GameObject playerPrefab;


  private Timekeeper timekeeper;
  private MemoryComponent memComponent;
  private float lastPressTime = 0f;


  public enum State {
    walking,
    transitioning,
    traveling
  }

  // Use this for initialization
  void Start()
  {
    // Get and store a reference to the Rigidbody2D component so that we can
    // access it.
    memComponent = currentPlayer.GetComponent<MemoryComponent> ();
  }

  void Awake() {}

  // FixedUpdate is called at a fixed interval and is independent of frame rate.
  // Put physics code here.
  void Update()
  {
    if (timekeeper == null) {
      timekeeper = Timekeeper.getInstance();
    }

    if (!underControl) {
      return;
    }

    // only accept input after a cooldown

    bool travel = Input.GetKeyUp("space");

    switch (state) {
    case State.walking:

      if (travel) {
        lastPressTime = timekeeper.getTime();
        willStartTraveling();
      }

      if (timekeeper.getTime() - lastPressTime < cooldown) {
        return;
      }

      // Store the current horizontal input in the float moveHorizontal.
      int moveHorizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));

      // Store the current vertical input in the float moveVertical.
      int moveVertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));


      if (moveHorizontal != 0) {
        lastPressTime = timekeeper.getTime();

        // direction = 1 if right (moveHorizontal  =1)
        // direction = 3 if left (moveHorizontal = -1)
        memComponent.deltaPosition(0, moveHorizontal, 1 - moveHorizontal);
      } else if (moveVertical != 0) {
        lastPressTime = timekeeper.getTime();

        // direction = 2 if up (moveVertical  =1)
        // direction = 4 if down (moveVertical = -1)
        memComponent.deltaPosition(moveVertical, 0, 2 - moveVertical);
      }
      break;

    case State.traveling:

      if (travel) {
        lastPressTime = timekeeper.getTime();
        willStartWalking();
      }

      // Store the current horizontal input in the float moveHorizontal.
      float moveTime = Input.GetAxis("Horizontal");

      if (moveTime != 0) {
        lastPressTime = timekeeper.getTime();
        timekeeper.immediateOffset(0.1f * moveTime);
      }

      break;

    // Handle transitions
    case State.transitioning:
      float deltaTime = timekeeper.getTime() - lastPressTime;
      memComponent.progress = deltaTime / transitionTime;

      if (memComponent.progress < 1) {
        return;
      }

      // Finished transition
      if (memComponent.state == Memory.MemoryEvent.appearing) {
        startWalking();
        return;
      } else if (memComponent.state == Memory.MemoryEvent.disappearing) {
        startTraveling();
        return;
      } else {
        Debug.Log("Invalid state in PlayerControllerSystem");
      }
      break;
    }
  }

  void willStartTraveling() {
    state                 = State.transitioning;
    memComponent.progress = 0;
    memComponent.state    = Memory.MemoryEvent.disappearing;
    currentPlayer.GetComponent<MovementSystem>().startTransition(transitionTime);
    Behaviour halo = (Behaviour)currentPlayer.GetComponent("Halo");
    halo.enabled = false;
  }

  void willStartWalking() {
    // set inactive state
    memComponent.SetInactive();
    currentPlayer.GetComponent<MemorySystem>().ImmediateSave(
      timekeeper.getTick() - 1);
    memComponent.firstActiveTick = timekeeper.getTick() - 1;

    memComponent.isSaving = true;
    state                 = State.transitioning;
    memComponent.state    = Memory.MemoryEvent.appearing;
    memComponent.progress = 0.1f;
    currentPlayer.GetComponent<MovementSystem>().startTransition(transitionTime);
    timekeeper.stopRewind();
  }

  void startTraveling() {
    // Save inactive state
    memComponent.SetInactive();
    currentPlayer.GetComponent<MemorySystem>().ImmediateSave();

    memComponent.isSaving       = false;
    memComponent.lastActiveTick = timekeeper.getTick();
    memComponent.state          = Memory.MemoryEvent.reposition;
    GameObject newPlayer = Instantiate(currentPlayer);

    MemoryComponent newMemoryComponent =
      newPlayer.GetComponent<MemoryComponent> ();

    currentPlayer = newPlayer;
    memComponent  = newMemoryComponent;

    Behaviour halo = (Behaviour)currentPlayer.GetComponent("Halo");
    halo.enabled = true;

    memComponent.state    = Memory.MemoryEvent.appearing;
    memComponent.progress = 0.1f;

    state = State.traveling;
    timekeeper.startRewind();
  }

  void startWalking() {
    memComponent.state = Memory.MemoryEvent.reposition;
    state              = State.walking;
  }

  // OnTriggerEnter2D is called whenever this object overlaps with a trigger
  // collider.
  //	void OnTriggerEnter2D(Collider2D other)
  //	{
  //		//Check the provided Collider2D parameter other to see if it is
  // tagged "PickUp", if it is...
  //		if (other.gameObject.CompareTag ("PickUp")) {
  //			//... then set the other object we just collided with to
  // inactive.
  //			other.gameObject.SetActive (false);
  //
  //
  //			Respawn (other.gameObject);
  //		} else if (other.gameObject.CompareTag ("Hazard")) {
  //			other.gameObject.SetActive (false);
  //
  //		}
  //	}
  //	void Respawn(GameObject gameObject) {
  //		float newX = Random.Range (-11, 11);
  //		float newY = Random.Range (-11, 11);
  //		Vector2 newPosition = new Vector2 (newX, newY);
  //		gameObject.GetComponent<Transform> ().position = newPosition;
  //
  //		gameObject.SetActive (true);
  //	}
}
