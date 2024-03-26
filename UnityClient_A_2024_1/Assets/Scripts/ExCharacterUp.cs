using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExCharacterUp :Excharacter
{
   protected override void Move()
    {
        transform.Translate(
            Vector3.up * speed * 2
            * Time.deltaTime);
    }
}
