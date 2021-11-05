using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Bird {
    [SerializeField]
    public float _boostForce = 100;
    public bool _hasSplit = false;
    public bool isOriginal = true;
    public Rigidbody2D _rigidbody2D;
    public float forceVariation = 30;

    public void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (isOriginal) {
            BaseStart();
        }
    }

    public override void OnTap() {
        BlueBird clone;
        Rigidbody2D cloneRigidbody2D;

        if (!_hasSplit) {
            _hasSplit = true;

            for (int Loop = 0; Loop < 2; Loop++) {
                clone = GameObject.Instantiate(gameObject).GetComponent<BlueBird>();
                clone.isOriginal = false;
                clone.transform.position = gameObject.transform.position;

                cloneRigidbody2D = clone.GetComponent<Rigidbody2D>();
                cloneRigidbody2D.velocity = _rigidbody2D.velocity;
                cloneRigidbody2D.angularVelocity = _rigidbody2D.angularVelocity;

                if (Loop == 0)
                    cloneRigidbody2D.AddForce(transform.up * forceVariation);
                else if (Loop == 1)
                    cloneRigidbody2D.AddForce(-transform.up * forceVariation);
            }
        }
    }

    void OnDestroy() {
        if (isOriginal) {
            BaseDestroy();
        }
    }
}
