//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    private Renderer _myRenderer;
    private Vector3 _initialPosition;
    private Dictionary<string, Figure> _figuresInformation = new Dictionary<string, Figure>();
    private TMPro.TextMeshProUGUI _figureName;
    private TMPro.TextMeshProUGUI _edgesTextMesh;
    private TMPro.TextMeshProUGUI _verticesTextMesh;
    private TMPro.TextMeshProUGUI _facesTextMesh;
    private TMPro.TextMeshProUGUI _volumeFormulaTextMesh;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        _myRenderer.material = InactiveMaterial;
        _initialPosition = transform.position;

        _figureName = GameObject.FindGameObjectWithTag("FigureName").GetComponent<TMPro.TextMeshProUGUI>();
        _edgesTextMesh = GameObject.FindGameObjectWithTag("Edges").GetComponent<TMPro.TextMeshProUGUI>();
        _verticesTextMesh = GameObject.FindGameObjectWithTag("Vertices").GetComponent<TMPro.TextMeshProUGUI>();
        _facesTextMesh = GameObject.FindGameObjectWithTag("Faces").GetComponent<TMPro.TextMeshProUGUI>();
        _volumeFormulaTextMesh = GameObject.FindGameObjectWithTag("VolumeFormula").GetComponent<TMPro.TextMeshProUGUI>();

        _figuresInformation.Add("Sphere", new Figure() { Name = "Esfera", Edges = "Sin aristas", Faces = "Caras: 1", Vertices = "Sin vertices", VolumeFormula = "V = ¾πr^3" });
        _figuresInformation.Add("Pyramid", new Figure() { Name = "Pirámide", Edges = "Aristas: 8", Faces = "Caras: 5", Vertices = "Vertices: 5", VolumeFormula = "V = ⅓l^2 * h" });
        _figuresInformation.Add("Icosaedron", new Figure() { Name = "Icosaedro", Edges = "Aristas: 30", Faces = "Caras: 20", Vertices = "Vertices: 12", VolumeFormula = "V = (5/12)*(3+√5)*l^3" });
        _figuresInformation.Add("Cube", new Figure() { Name = "Cubo", Edges = "Aristas: 12", Faces = "Caras: 6", Vertices = "Vertices: 8", VolumeFormula = "l^3" });
        _figuresInformation.Add("Cone", new Figure() { Name = "Cono", Edges = "Aristas: 1", Faces = "Caras: 2", Vertices = "Vertices: 1", VolumeFormula = "V = (π * r^2 * h) / 3" });
        _figuresInformation.Add("Cylinder", new Figure() { Name = "Cilindro", Edges = "Aristas: 3", Faces = "Caras: 3", Vertices = "Vertices: 2", VolumeFormula = "V = π * r^2 * h" });
    }

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        // Highlight just selected figure
        foreach (string figureTag in _figuresInformation.Keys)
        {
            if (figureTag == tag)
            {
                _myRenderer.material = GazedAtMaterial;
                transform.position = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z - 2);
            }
            else
            {
                Renderer figureRenderer = GameObject.FindGameObjectWithTag(figureTag).GetComponent<Renderer>();
                figureRenderer.material = InactiveMaterial;
                figureRenderer.transform.position = new Vector3(figureRenderer.transform.position.x, figureRenderer.transform.position.y, _initialPosition.z);
            }
        }

        Figure selectedFigure;
        bool figureFound = _figuresInformation.TryGetValue(tag, out selectedFigure);

        if (figureFound)
        {
            _figureName.text = selectedFigure.Name;
            _edgesTextMesh.text = selectedFigure.Edges;
            _verticesTextMesh.text = selectedFigure.Vertices;
            _facesTextMesh.text = selectedFigure.Faces;
            _volumeFormulaTextMesh.text = selectedFigure.VolumeFormula;
        }
        else
        {
            _figureName.text = tag;
        }
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
    }

    public void Update()
    {
        if (transform.position.z != _initialPosition.z)
        {
            transform.Rotate(new Vector3(30.0f, 30.0f, 30.0f) * Time.deltaTime);
        }
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
    }
}

class Figure
{
    public string Name;
    public string Edges;
    public string Vertices;
    public string Faces;
    public string VolumeFormula;
}

