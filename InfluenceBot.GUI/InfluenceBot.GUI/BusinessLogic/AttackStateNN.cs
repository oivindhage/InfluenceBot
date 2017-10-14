﻿using Accord.Neuro;
using Accord.Neuro.Learning;
using InfluenceBot.GUI.Model;
using System.Linq;

namespace InfluenceBot.GUI.BusinessLogic
{
    public class AttackStateNN
    {
        public ActivationNetwork network;
        public ResilientBackpropagationLearning teacher;

        public AttackStateNN()
        {
            var activationFunction = new Accord.Neuro.ActivationFunctions.GaussianFunction();
            network = new ActivationNetwork(new SigmoidFunction(0.01), 4 + 4 + 4 * 5 * 5, new[] { 1 });
            new GaussianWeights(network, 0.1).Randomize();
            teacher = new ResilientBackpropagationLearning(network);
            teacher.LearningRate = 0.1;
        }

        public double Evaluate(AttackState attackState)
            => network.Compute(attackState.State).First();
    }
}