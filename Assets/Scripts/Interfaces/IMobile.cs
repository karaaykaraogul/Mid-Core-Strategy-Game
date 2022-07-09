using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMobile
{
    void Interaction();
    void DeselectUnit();
    void CancelInteraction();
    void SetSpawnPath(Vector3 spawnPoint);
    bool GetSelectStatus();
    float GetSpeed();
    void SetPath();
}
